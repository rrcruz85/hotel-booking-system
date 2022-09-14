using Hotel.Booking.Contract;

namespace Hotel.Management.DataAccess.Entities
{
    public partial class Hotel : IEntity
    {
        public Hotel()
        {
            HotelContactInfos = new HashSet<HotelContactInfo>();
            HotelFacilities = new HashSet<HotelFacility>();
            HotelGalleries = new HashSet<HotelGallery>();
            HotelServices = new HashSet<HotelService>();
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int LocationId { get; set; }
        public int? CategoryId { get; set; }

        public virtual HotelCategory? Category { get; set; }
        public virtual Location Location { get; set; } = null!;
        public virtual ICollection<HotelContactInfo> HotelContactInfos { get; set; }
        public virtual ICollection<HotelFacility> HotelFacilities { get; set; }
        public virtual ICollection<HotelGallery> HotelGalleries { get; set; }
        public virtual ICollection<HotelService> HotelServices { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
