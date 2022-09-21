using System.ComponentModel.DataAnnotations;

namespace Hotel.Management.WebApi.Models.Requests
{
    public class UpdateHotelContactInfo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        [MinLength(1)]
        public string Value { get; set; } = null!;
        [Required]
        public int HotelId { get; set; }
    }
}
