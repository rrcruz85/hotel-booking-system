using Hotel.Booking.Common.Constant;
using Hotel.Booking.Common.Contract.Messaging;
using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Translators;
using Microsoft.Extensions.Configuration;
using Reservation.Management.Service.Interfaces;

namespace Reservation.Management.Service.Implementations
{
    public class RerservationService : IReservationService
    {
        private readonly IReservationRuleService _reservationRuleService;
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomReservationRepository _roomReservationRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfiguration _config;
        private readonly string RoomTopicName = "RoomTopicName";
        private readonly string ReservationTopicName = "ReservationTopicName";

        public RerservationService(
            IReservationRuleService reservationRuleService,
            IReservationRepository reservationRepository,
            IRoomReservationRepository roomReservationRepository,
            IMessagingEngine messagingEngine, 
            IConfiguration config)
        {
            _reservationRuleService = reservationRuleService;
            _reservationRepository = reservationRepository;
            _roomReservationRepository = roomReservationRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }

        public async Task<int> CreateReservationAsync(CreateReservation reservation)
        {
            var validationResult = await _reservationRuleService.CheckRulesOnCreateAsync(reservation);
            if (!validationResult.Ok)
            {
                throw new ArgumentException(validationResult.Message);
            }

            var entity = reservation.ToEntity();
            var reservationId = await _reservationRepository.AddAsync(entity);
            entity.Id = reservationId;
            await _messagingEngine.PublishEventMessageAsync(ReservationTopicName, (int)ReservationEventType.Created, entity);

            var roomReservations = reservation.Rooms.Select(r => new DataAccess.Entities.RoomReservation
            {
               DiscountPrice = r.DiscountPrice,
               Price = r.Price,
               ReservationId = reservationId,
               RoomId = r.RoomId
            }).ToList();

            await _roomReservationRepository.AddMultipleAsync(roomReservations);

            return reservationId;
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            var entity = await _reservationRepository.FirstOrDefaultAsync(r => r.Id == reservationId);
            if (entity == null)
            {
                throw new ArgumentException($"reservation {reservationId} not found");
            }

            if (entity.Status != (int)ReservationStatus.Canceled && entity.StartDate <= DateTime.Now && entity.EndDate >= DateTime.Now)
            {
                foreach(var r in entity.RoomReservations.Select(r => new { r.RoomId, RoomStatus = RoomStatus.Available}).ToList())
                {
                    await _messagingEngine.PublishEventMessageAsync(RoomTopicName, (int)RoomEventType.Available, r);
                }
            }

            await _reservationRepository.DeleteAsync(entity);
            await _messagingEngine.PublishEventMessageAsync(ReservationTopicName, (int)ReservationEventType.Deleted, entity);
        }

        public async Task<Model.Reservation?> GetReservationByIdAsync(int reservationId)
        {
            var entity = await _reservationRepository.FirstOrDefaultAsync(r => r.Id == reservationId);
            return entity?.ToModel();
        }

        public Task<Model.Reservation?> GetReservationDetailsByIdAsync(int reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Model.Reservation>> GetReservationsByHotelIdAsync(int hotelId)
        {
            var entities = await _reservationRepository.WhereAsync(r => r.RoomReservations.Any(rr => rr.ReservationNavigation.HotelId == hotelId));
            return entities.OrderByDescending(r => r.StartDate).Select(r => r.ToModel()).ToList();
        }

        public async Task<List<Model.Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            var entities = await _reservationRepository.WhereAsync(r => r.UserId == userId);
            return entities.OrderByDescending(r => r.StartDate).Select(r => r.ToModel()).ToList();
        }

        public Task UpdateReservationAsync(Model.Reservation room)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateReservationStatusAsync(int reservationId, int status, string observations)
        {
            var entity = await _reservationRepository.SingleOrDefaultAsync(c => c.Id == reservationId);
            if (entity == null)
            {
                throw new ArgumentException($"Reservation {reservationId} does not exist");
            }            
            entity.Status = status;
            entity.Observations = observations;
            await _reservationRepository.UpdateAsync(entity);
            await _messagingEngine.PublishEventMessageAsync(_config[ReservationTopicName], (int)ReservationEventType.Updated, entity);
        }
    }
}
