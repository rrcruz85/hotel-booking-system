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

        public Task DeleteReservationAsync(int reservationId)
        {
            throw new NotImplementedException();
        }

        public Task<Model.Reservation?> GetReservationByIdAsync(int reservationId)
        {
            throw new NotImplementedException();
        }

        public Task<Model.Reservation?> GetReservationDeatilsByIdAsync(int reservationId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Model.Reservation>> GetReservationsByHotelIdAsync(int hotelId)
        {
            throw new NotImplementedException();
        }

        public Task<Room?> GetRoomByNumberAsync(int hotelId, int number)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetRoomsByStatusAndHotelAsync(int hotelId, int status)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetRoomsByTypeAndHotelAsync(int hotelId, int type)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReservationAsync(Model.Reservation room)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReservationStatusAsync(int reservationId, int status, string observations)
        {
            throw new NotImplementedException();
        }
    }
}
