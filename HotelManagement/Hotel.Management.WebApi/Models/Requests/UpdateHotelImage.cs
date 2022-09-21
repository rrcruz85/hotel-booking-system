using System.ComponentModel.DataAnnotations;

namespace Hotel.Management.WebApi.Models.Requests
{
    public class UpdateHotelImage
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        [Display(Description = "Base64 Image Content, file must be png format")]
        public string BlobImageContent { get; set; } = null!;
        public string? Description { get; set; }
    }
}
