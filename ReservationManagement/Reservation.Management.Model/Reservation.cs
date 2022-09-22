
namespace Reservation.Management.Model
{
    public class CreateReservation: IReservationContext
    {
        public int HotelId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public string? Observations { get; set; }
        public int? PaymentMethodType { get; set; }
        public string? PaymentMethodInfo { get; set; }
        public List<CreateRoomReservation> Rooms { get; set; }
        IEnumerable<IRoomReservationContext> IReservationContext.Rooms => Rooms;
    }

    public class CreateRoomReservation: IRoomReservationContext
    {
        public int RoomId { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
    }

    public class ReservationRuleValidationResponse : IReservationRuleValidationResponse
    {
        public bool Ok { get; set ; } = true;
        public string Message { get; set; } = string.Empty;
    }

    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public string? Observations { get; set; }
        public int Status { get; set; }
        public int? PaymentMethodType { get; set; }
        public string? PaymentMethodInfo { get; set; }
    }
}
