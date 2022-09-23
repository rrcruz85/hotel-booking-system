using Hotel.Booking.Common.Contract.DataAccess;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class RoomReservation: IEntity
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
