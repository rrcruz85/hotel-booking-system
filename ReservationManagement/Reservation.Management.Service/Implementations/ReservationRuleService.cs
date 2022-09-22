using Hotel.Booking.Common.Constant;
using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Reservation.Management.Service.Implementations
{
    public class ReservationRuleService : IReservationRuleService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly int ReservationDurationInDaysLimit = 3;
        private readonly int DaysInAdvanceForReservationLimit = 30;
        private readonly int ReservationMinStartDaysLimit = 1;

        public ReservationRuleService(
            IRoomRepository roomRepository,
            IReservationRepository reservationRepository)
        {
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<bool> CheckRoomsAvailabiltyAsync(IReservationContext reservation)
        {
            if (reservation.EndDate <= DateTime.Now || (reservation.StartDate <= DateTime.Now && reservation.EndDate >= DateTime.Now))
            {
                return false;
            }           

            var roomIds = reservation.Rooms.Select(r => r.RoomId).ToList();
            var isThereAnyReservation = await _roomRepository.WhereQueryable(r => r.HotelId == reservation.HotelId
                 && roomIds.Contains(r.Id) 
                 && r.RoomReservations.Any(rr => rr.Reservation.Status != (int)ReservationStatus.Canceled 
                 && (rr.Reservation.StartDate <= reservation.EndDate && rr.Reservation.EndDate >= reservation.StartDate)))
                .AnyAsync();
            return !isThereAnyReservation;
        }

        public async Task<IReservationRuleValidationResponse> CheckRulesOnCreateAsync(IReservationContext reservation)
        {
            var response = CheckRules(reservation);

            var roomsAreAvailable = await CheckRoomsAvailabiltyAsync(reservation);
            if (!roomsAreAvailable)
            {
                response.Ok = false;
                response.Message = $"Rooms are not available in the selectect range of dates";
            }

            return response;
        }

        public async Task<IReservationRuleValidationResponse> CheckRulesOnUpdateAsync(IEditReservationContext reservation)
        {
            var response = CheckRules(reservation);

            var currentReservation = await _reservationRepository.FirstOrDefaultAsync(r => r.Id == reservation.ReservationId);
 
            if (currentReservation.Status != (int)ReservationStatus.Canceled && currentReservation.EndDate <= DateTime.Now)
            {
                response.Ok = false;
                response.Message = "Reservation can not be modified because it is already past due";
                return response;
            }

            return response;
        }        

        private IReservationRuleValidationResponse CheckRules(IReservationContext reservation)
        {
            var response = new ReservationRuleValidationResponse();

            if (reservation.StartDate >= reservation.EndDate)
            {
                response.Ok = false;
                response.Message = "Start Date must be lower than or equal to End Date";
                return response;
            }

            var duration = reservation.EndDate.Subtract(reservation.StartDate);
            if (duration.Days > ReservationDurationInDaysLimit)
            {
                response.Ok = false;
                response.Message = $"Reservation can not last more than {ReservationDurationInDaysLimit} days";
                return response;
            }

            if (reservation.StartDate >= DateTime.Now.AddDays(DaysInAdvanceForReservationLimit))
            {
                response.Ok = false;
                response.Message = $"Reservation can not be scheduled with more than {DaysInAdvanceForReservationLimit} days in advance";
                return response;
            }

            var minStartDate = DateTime.Now.AddDays(ReservationMinStartDaysLimit);
            if (reservation.StartDate <= minStartDate)
            {
                response.Ok = false;
                response.Message = $"Reservation can not start before than {minStartDate}";
            }

            return response;
        }
    }
}
