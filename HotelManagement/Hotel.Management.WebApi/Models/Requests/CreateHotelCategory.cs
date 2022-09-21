using System.ComponentModel.DataAnnotations;

namespace Hotel.Management.WebApi.Models.Requests
{
    public class CreateHotelCategory
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; } 
        public string? Description { get; set; }
    }
}
