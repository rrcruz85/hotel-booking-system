using System.ComponentModel.DataAnnotations;

namespace Hotel.Management.WebApi.Models.Requests
{
    public class UpdateRoom
    {
        [Required]
        public int Id { get; set; }
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
        [Required]
        public int Status { get; set; }
        public int? Floor { get; set; }
    }
}
