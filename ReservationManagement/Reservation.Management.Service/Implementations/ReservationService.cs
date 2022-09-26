using Hotel.Booking.Common.Constant;
using Hotel.Booking.Common.Contract.Messaging;
using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Translators;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Model.Event;
using Hotel.Booking.Common.Contract.Services;

namespace Reservation.Management.Service.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRuleService _reservationRuleService;
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationHistoryRepository _reservationHistoryRepository;
        private readonly IRoomReservationRepository _roomReservationRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfigurationView _config;
        private readonly string RoomTopicName = "RoomTopicName";       

        public ReservationService(
            IReservationRuleService reservationRuleService,
            IReservationRepository reservationRepository,
            IReservationHistoryRepository reservationHistoryRepository,
            IRoomReservationRepository roomReservationRepository,
            IMessagingEngine messagingEngine, 
            IConfigurationView config)
        {
            _reservationRuleService = reservationRuleService;
            _reservationRepository = reservationRepository;
            _roomReservationRepository = roomReservationRepository;
            _reservationHistoryRepository = reservationHistoryRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }

        public async Task<int> CreateReservationAsync(CreateUpdateReservation reservation)
        {
            var validationResult = await _reservationRuleService.CheckRulesOnCreateAsync(reservation);
            if (!validationResult.Ok)
            {
                throw new ArgumentException(validationResult.Message);
            }

            var entity = reservation.ToNewEntity();
            var reservationId = await _reservationRepository.AddAsync(entity);
            entity.Id = reservationId;
          
            var roomReservations = reservation.Rooms.Select(r => new DataAccess.Entities.RoomReservation
            {
               DiscountPrice = r.DiscountPrice,
               Price = r.Price,
               ReservationId = reservationId,
               RoomId = r.RoomId
            }).ToList();

            await _roomReservationRepository.AddMultipleAsync(roomReservations);

            await _reservationHistoryRepository.AddAsync(new DataAccess.Entities.ReservationHistory
            {
                ReservationId = reservationId,
                CreatedDateTime = DateTime.Now,
                Status = (int)ReservationStatus.Booked,
                UserId = reservation.UserId
            });

            foreach (var @event in reservation.Rooms.Select(r => new RoomStatusEvent { RoomId = r.RoomId, Status = (int)RoomStatus.Booked}).ToList())
            {
                await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(RoomTopicName), (int)RoomEventType.Booked, @event);
            }

            return reservationId;
        }

        public async Task DeleteReservationAsync(int reservationId, int userId)
        {
            var entity = await _reservationRepository.FirstOrDefaultAsync(r => r.Id == reservationId);
            if (entity == null)
            {
                throw new ArgumentException($"reservation {reservationId} not found");
            }

            if (entity.Status != (int)ReservationStatus.Canceled && entity.StartDate <= DateTime.Now && entity.EndDate >= DateTime.Now)
            {
                throw new ArgumentException($"Ongoing reservation can not be changed");
            }

            var rooms = await _roomReservationRepository.WhereAsync(r => r.ReservationId == reservationId);
            await _roomReservationRepository.DeleteMultipleAsync(rooms);

            await _reservationRepository.DeleteAsync(entity);

            await _reservationHistoryRepository.AddAsync(new DataAccess.Entities.ReservationHistory
            {
                ReservationId = reservationId,
                CreatedDateTime = DateTime.Now,
                Status = (int)ReservationStatus.Deleted,
                UserId = userId
            });

            foreach (var @event in entity.RoomReservations.Select(r => new RoomStatusEvent{ RoomId = r.RoomId, Status = (int)RoomStatus.Available }).ToList())
            {
                await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(RoomTopicName), (int)RoomEventType.Available, @event);
            }            
        }

        public async Task<Model.Reservation?> GetReservationByIdAsync(int reservationId)
        {
            var entity = await _reservationRepository.FirstOrDefaultAsync(r => r.Id == reservationId);
            return entity?.ToBaseModel();
        }

        public async Task<ReservationDetails?> GetReservationDetailsByIdAsync(int reservationId)
        {
            var entity = await _reservationRepository.GetWithRelationsAsync(reservationId);
            return entity?.ToDetailModel();
        }

        public async Task<List<ReservationDetails>> GetReservationsByHotelIdAsync(int hotelId)
        {
            var entities = await _reservationRepository.GetByHotelIdWithRelationsAsync(hotelId);
            return entities.OrderByDescending(r => r.StartDate).Select(r => r.ToDetailModel()).ToList();
        }

        public async Task<List<ReservationDetails>> GetReservationsByUserIdAsync(int userId)
        {
            var entities = await _reservationRepository.GetByUserIdWithRelationsAsync(userId);
            return entities.OrderByDescending(r => r.StartDate).Select(r => r.ToDetailModel()).ToList();
        }

        public async Task<List<ReservationDetails>> GetReservationsByHotelIdAndDatesAsync(int hotelId, DateTime startDate, DateTime endDate)
        {
            var entities = await _reservationRepository.GetReservationsByHotelAndDatesAsync(hotelId, startDate, endDate);
            return entities.OrderByDescending(r => r.StartDate).Select(r => r.ToDetailModel()).ToList();
        }

        public async Task UpdateReservationAsync(CreateUpdateReservation reservation)
        {
            var entity = await _reservationRepository.GetWithRelationsAsync(reservation.ReservationId);
            if (entity == null)
            {
                throw new ArgumentException($"Reservation {reservation.ReservationId} does not exist");
            }

            if (entity.Status == (int)ReservationStatus.Canceled)
            {
                throw new ArgumentException($"Canceled reservation can not modified");
            }

            if (entity.StartDate <= DateTime.Now && reservation.StartDate.Date != entity.StartDate.Date)
            {
                throw new ArgumentException($"Ongoing reservation can not be modified");
            }

            var oldBookedRooms = entity.RoomReservations.Select(rr => rr.RoomId).ToList();
            var newBookedRooms = reservation.Rooms.Select(r => r.RoomId).ToList();

            var removedRooms = oldBookedRooms.Except(newBookedRooms).ToList();
            var newRooms = newBookedRooms.Except(oldBookedRooms).ToList();
            
            if (removedRooms.Any() || newRooms.Any() || reservation.StartDate.Date != entity.StartDate.Date || reservation.EndDate.Date != entity.EndDate.Date)
            {
                var validationResult = await _reservationRuleService.CheckRulesOnUpdateAsync(reservation);
                if (!validationResult.Ok)
                {
                    throw new ArgumentException(validationResult.Message);
                }
            }

            var statusHasChanged = entity.Status != reservation.Status;

            await _reservationRepository.UpdateAsync(entity.UpdateFromModel(reservation));

            if (statusHasChanged)
            {
                await _reservationHistoryRepository.AddAsync(new DataAccess.Entities.ReservationHistory
                {
                    ReservationId = reservation.ReservationId,
                    CreatedDateTime = DateTime.Now,
                    Status = reservation.Status,
                    UserId = reservation.UserId
                });
            }

            // Removed rooms
            if (removedRooms.Any())
            {
                var roomReservations = entity.RoomReservations.Where(r => removedRooms.Contains(r.RoomId)).ToList();
                await _roomReservationRepository.DeleteMultipleAsync(roomReservations);
                foreach(var @event in removedRooms.Select(roomId => new RoomStatusEvent { RoomId = roomId , Status = (int)RoomStatus.Available}).ToList())
                {
                    await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(RoomTopicName), (int)RoomEventType.Available, @event);
                }
            }

            // new booked rooms
            if (newRooms.Any())
            {
                foreach(var roomId in newRooms)
                {
                    var @room = reservation.Rooms.FirstOrDefault(r => r.RoomId == roomId);
                    var roomReservation = new DataAccess.Entities.RoomReservation 
                    { 
                        DiscountPrice = @room.DiscountPrice,
                        Price = @room.Price,
                        ReservationId = reservation.ReservationId,
                        RoomId = roomId,
                    };

                    await _roomReservationRepository.AddAsync(roomReservation);
                    var @event = new RoomStatusEvent { RoomId = roomId, Status = (int)RoomStatus.Booked };
                    await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(RoomTopicName), (int)RoomEventType.Booked, @event);
                }                
            }

            // Updating price and discount
            var commonRoomReservations = oldBookedRooms.Intersect(newBookedRooms).ToList();
            foreach(var roomId in commonRoomReservations)
            {
                var @roomReservation = reservation.Rooms.FirstOrDefault(r => r.RoomId == roomId);
                var @roomReservationEntity = entity.RoomReservations.FirstOrDefault(r => r.RoomId == roomId);
                if (@roomReservation != null && @roomReservationEntity != null && (@roomReservation.Price != @roomReservationEntity.Price || @roomReservation.DiscountPrice != @roomReservationEntity.DiscountPrice))
                {
                    if (@roomReservationEntity.Price != @roomReservation.Price)
                        @roomReservationEntity.Price = @roomReservation.Price;
                    if (@roomReservationEntity.DiscountPrice != @roomReservation.DiscountPrice)
                        @roomReservationEntity.DiscountPrice = @roomReservation.DiscountPrice;
                    await _roomReservationRepository.UpdateAsync(@roomReservationEntity);
                }
            }                    
        }

        public async Task UpdateReservationStatusAsync(int reservationId, int status, int userId, string observations)
        {
            var entity = await _reservationRepository.FirstOrDefaultAsync(c => c.Id == reservationId);
            if (entity == null)
            {
                throw new ArgumentException($"Reservation {reservationId} does not exist");
            }
            if (entity.Status == (int)ReservationStatus.Canceled)
            {
                throw new ArgumentException($"Canceled reservation can not modified");
            }
            if (entity.Status == status)
            {
                throw new ArgumentException($"Reservation status has not changed");
            }
            entity.Status = status;
            entity.Observations = observations;
            await _reservationRepository.UpdateAsync(entity);

            await _reservationHistoryRepository.AddAsync(new DataAccess.Entities.ReservationHistory
            {
                ReservationId = reservationId,
                CreatedDateTime = DateTime.Now,
                Status = status,
                UserId = userId
            });
        }
    }
}
