using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class City: IEntity
    {
        public City()
        {
            Hotels = new HashSet<Hotel>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;

        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
