using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace UserAccount.Management.DataAccess.Entities
{
    public partial class User: IEntity
    {
        public User()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
