using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Hotel.Management.DataAccess.Entities
{
    public partial class HotelContactInfo: IEntity
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Value { get; set; } = null!;
        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
    }
}
