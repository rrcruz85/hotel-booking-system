using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class Hotel: IEntity
    {
        public Hotel()
        {
            HotelCategoryRelations = new HashSet<HotelCategoryRelation>();
            HotelContactInfos = new HashSet<HotelContactInfo>();
            HotelFacilities = new HashSet<HotelFacility>();
            HotelGalleries = new HashSet<HotelGallery>();
            HotelServices = new HashSet<HotelService>();
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string? Zip { get; set; }
        public int CityId { get; set; }
        public string? GeoLocation { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<HotelCategoryRelation> HotelCategoryRelations { get; set; }
        public virtual ICollection<HotelContactInfo> HotelContactInfos { get; set; }
        public virtual ICollection<HotelFacility> HotelFacilities { get; set; }
        public virtual ICollection<HotelGallery> HotelGalleries { get; set; }
        public virtual ICollection<HotelService> HotelServices { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
