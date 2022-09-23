using Reservation.Management.Model;
using Reservation.Management.WebApi.Models.Requests;
using System.ComponentModel.DataAnnotations;

namespace Reservation.Management.WebApi.Translator
{
    public static class Reservation
    {
        public static CreateUpdateReservation ToBusinessModel(this CreateReservation reservation)
        {
            return new CreateUpdateReservation
            {
                EndDate = reservation.EndDate,
                StartDate = reservation.StartDate,
                HotelId = reservation.HotelId,
                Observations = reservation.Observations,
                PaymentMethodInfo = reservation.PaymentMethodInfo,
                PaymentMethodType = reservation.PaymentMethodType,
                UserId = reservation.UserId
            };
        }

        public static CreateUpdateReservation ToBusinessModel(this UpdateReservation reservation)
        {
            return new CreateUpdateReservation
            {
                EndDate = reservation.EndDate,
                StartDate = reservation.StartDate,
                HotelId = reservation.HotelId,
                Observations = reservation.Observations,
                PaymentMethodInfo = reservation.PaymentMethodInfo,
                PaymentMethodType = reservation.PaymentMethodType,
                UserId = reservation.UserId,
                ReservationId = reservation.ReservationId,
                Status = reservation.Status,
                Rooms = reservation.Rooms.Select(r => new CreateUpdateRoomReservation
                {
                    DiscountPrice = r.DiscountPrice,
                    Price = r.Price,    
                    RoomId = r.RoomId,
                }).ToList()
            };
        }

    }
}
