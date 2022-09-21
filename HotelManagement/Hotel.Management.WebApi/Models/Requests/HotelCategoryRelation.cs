using System.ComponentModel.DataAnnotations;

namespace Hotel.Management.WebApi.Models.Requests
{
    public class HotelCategoryRelation
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int HotelId { get; set; }
    }
}
