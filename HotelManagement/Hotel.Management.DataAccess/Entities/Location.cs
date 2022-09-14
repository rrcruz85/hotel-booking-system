using Hotel.Booking.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Hotel.Management.DataAccess.Entities
{
    public partial class Location : IEntity
    {
        public Location()
        {
            Hotels = new HashSet<Hotel>();
        }

        public int Id { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string Zip { get; set; } = null!;
        public int CityId { get; set; }
        public string? GeoLocation { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
