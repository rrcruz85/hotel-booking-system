using System.ComponentModel.DataAnnotations;

namespace Hotel.Management.WebApi.Models.Requests
{
    public class CreateRoom
    {
        [Required]
        public int HotelId { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public int MaxCapacity { get; set; }
        [Required]
        public decimal CurrentPrice { get; set; }
        [Required]
        public int Type { get; set; }
        public int? Floor { get; set; }
    }
}
