
namespace Hotel.Management.Model
{
    public class HotelDetails : Hotel 
    {
        public HotelDetails()
        {
            HotelCategoryRelations = new HashSet<HotelCategoryRelation>();
            HotelContactInfos = new HashSet<HotelContactInfo>();
            HotelFacilities = new HashSet<HotelFacility>();
            HotelGalleries = new HashSet<HotelGallery>();
            HotelServices = new HashSet<HotelService>();
            Rooms = new HashSet<Room>();
        }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<HotelCategoryRelation> HotelCategoryRelations { get; set; }
        public virtual ICollection<HotelContactInfo> HotelContactInfos { get; set; }
        public virtual ICollection<HotelFacility> HotelFacilities { get; set; }
        public virtual ICollection<HotelGallery> HotelGalleries { get; set; }
        public virtual ICollection<HotelService> HotelServices { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
