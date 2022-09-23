
namespace Reservation.Management.Model
{
    public class RoomReservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int ReservationId { get; set; }
    }
}
