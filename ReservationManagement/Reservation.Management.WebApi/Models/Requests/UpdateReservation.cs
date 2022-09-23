using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Reservation.Management.WebApi.Models.Requests
{
    [ExcludeFromCodeCoverage]
    public class UpdateReservation
    {
        [Required]
        public int ReservationId { get; set; }
        [Required]
        public int HotelId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int UserId { get; set; }         
        public string? Observations { get; set; }
        public int? PaymentMethodType { get; set; }
        public string? PaymentMethodInfo { get; set; }

        [Required]
        public List<UpdateRoomReservation> Rooms { get; set; }
        
        public class UpdateRoomReservation
        {
            [Required]
            public int RoomId { get; set; }
            [Required]
            public decimal Price { get; set; }
            public decimal? DiscountPrice { get; set; }
        }
    }
}
