using System.ComponentModel.DataAnnotations;

namespace Reservation.Management.WebApi.Models.Requests
{
    public class CreateReservation
    {
        [Required]
        public int HotelId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int UserId { get; set; }         
        public string? Observations { get; set; }
        public int? PaymentMethodType { get; set; }
        public string? PaymentMethodInfo { get; set; }

        [Required]
        public List<CreateRoomReservation> Rooms { get; set; }
        
        public class CreateRoomReservation
        {
            [Required]
            public int RoomId { get; set; }
            [Required]
            public decimal Price { get; set; }
            public decimal? DiscountPrice { get; set; }
        }
    }
}
