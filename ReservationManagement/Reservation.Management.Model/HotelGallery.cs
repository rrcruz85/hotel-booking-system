
namespace Reservation.Management.Model
{ 
    public class HotelGallery
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string BlobImageUri { get; set; } = null!;
        public string? Description { get; set; }
    }     
}
