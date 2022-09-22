
using Hotel.Booking.Common.Constant;

namespace Reservation.Management.Service.Translators
{
    public static class ReservationTranslator
    {
        public static DataAccess.Entities.Reservation ToEntity(this Model.CreateReservation reservation)
        {
            return new DataAccess.Entities.Reservation
            {            
                 EndDate = reservation.EndDate,
                 Observations = reservation.Observations,   
                 PaymentMethodInfo = reservation.PaymentMethodInfo,
                 PaymentMethodType = reservation.PaymentMethodType,
                 StartDate = reservation.StartDate,
                 Status = (int)ReservationStatus.Booked,
                 UserId = reservation.UserId
            };
        }
 
        public static Model.Reservation ToModel(this DataAccess.Entities.Reservation reservation)
        {
            return new Model.Reservation
            {
                Id = reservation.Id,
                EndDate = reservation.EndDate,
                Observations = reservation.Observations,
                PaymentMethodInfo = reservation.PaymentMethodInfo,
                PaymentMethodType = reservation.PaymentMethodType,
                StartDate = reservation.StartDate,
                Status = reservation.Status,
                UserId = reservation.UserId
            };
        }
    }
}
