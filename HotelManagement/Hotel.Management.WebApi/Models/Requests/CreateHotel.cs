using System.ComponentModel.DataAnnotations;

namespace Hotel.Management.WebApi.Models.Requests
{
    public class CreateHotel
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        public string AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? Zip { get; set; }

        [Required]
        public int CityId { get; set; }

        public string? GeoLocation { get; set; }
    }
}
