using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class User: IEntity
    {
        public User()
        {
            Reservations = new HashSet<Reservation>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string Passoword { get; set; } = null!;
        public bool IsActive { get; set; }
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
