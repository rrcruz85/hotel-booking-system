
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
 
        public static Model.Hotel ToModel(this DataAccess.Entities.Hotel hotel)
        {
            return new Model.Hotel
            {
                Id = hotel.Id,
                AddressLine1 = hotel.AddressLine1,
                AddressLine2 = hotel.AddressLine2,
                CityId = hotel.CityId,
                GeoLocation = hotel.GeoLocation,
                Name = hotel.Name,
                Zip = hotel.Zip
            };
        }
    }
}
