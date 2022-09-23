
namespace Reservation.Management.Model
{
    public interface IReservationContext
    {
        public int ReservationId { get; }
        public int HotelId { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public int UserId { get;}
        public IEnumerable<IRoomReservationContext> Rooms { get; }
    }

    public interface IRoomReservationContext
    {
        public int RoomId { get; }
    }

    public interface IReservationRuleValidationResponse
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
    }
}
