
namespace Hotel.Management.Model
{ 
    public class HotelGallery
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string BlobImageUri { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class CreateHotelImage
    {        
        public int HotelId { get; set; }        
        public string BlobImageContent { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class UpdateHotelImage
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string BlobImageContent { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class HotelImage
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string BlobImageUri { get; set; } = null!;
        public string BlobImageContent { get; set; } = null!;
        public string? Description { get; set; }
    }
}
