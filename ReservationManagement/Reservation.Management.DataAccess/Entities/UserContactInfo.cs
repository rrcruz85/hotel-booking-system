using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class UserContactInfo: IEntity
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Value { get; set; } = null!;
        public int ProfileId { get; set; }

        public virtual UserProfile Profile { get; set; } = null!;
    }
}
