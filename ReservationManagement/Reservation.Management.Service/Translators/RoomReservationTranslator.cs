using Reservation.Management.DataAccess.Entities;

namespace Reservation.Management.Service.Translators
{
    public static class RoomReservationTranslator
    {
        public static RoomReservation ToEntity(this Model.RoomReservation roomReservation)
        {
            return new RoomReservation
            {
                 DiscountPrice = roomReservation.DiscountPrice,
                 Id = roomReservation.Id,
                 Price = roomReservation.Price, 
                 ReservationId = roomReservation.ReservationId,
                 RoomId = roomReservation.RoomId
            };
        }         

        public static Model.RoomReservation ToModel(this RoomReservation roomReservation)
        {
            return new Model.RoomReservation
            {
                DiscountPrice = roomReservation.DiscountPrice,
                Id = roomReservation.Id,
                Price = roomReservation.Price,
                ReservationId = roomReservation.ReservationId,
                RoomId = roomReservation.RoomId
            };
        }
    }
}
