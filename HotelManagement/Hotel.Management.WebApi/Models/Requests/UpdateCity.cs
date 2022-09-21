using System.ComponentModel.DataAnnotations;

namespace Hotel.Management.WebApi.Models.Requests
{
    public class UpdateCity
    {
        [Required]        
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        public string State { get; set; }
        [Required]
        [MinLength(1)]
        public string Country { get; set; }
    }
}
