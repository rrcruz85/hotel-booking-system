
namespace Hotel.Management.Model
{ 
    public class HotelGallery
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string BlobImageUri { get; set; }
        public string Description { get; set; }
    }
}
