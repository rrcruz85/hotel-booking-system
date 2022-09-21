using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class Role: IEntity
    {
        public Role()
        {
            RolePermissions = new HashSet<RolePermission>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
