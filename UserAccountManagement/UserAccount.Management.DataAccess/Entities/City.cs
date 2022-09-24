using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace UserAccount.Management.DataAccess.Entities
{
    public partial class City: IEntity
    {
        public City()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
