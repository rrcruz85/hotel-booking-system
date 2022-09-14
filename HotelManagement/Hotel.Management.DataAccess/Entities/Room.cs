using Hotel.Booking.Contract;
using System;
using System.Collections.Generic;

namespace Hotel.Management.DataAccess.Entities
{
    public partial class Room : IEntity
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public int HotelId { get; set; }
        public int? Floor { get; set; }
        public string? Extension { get; set; }
        public int Type { get; set; }
        public int MaxCapacity { get; set; }
        public int Status { get; set; }
        public decimal CurrentPrice { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
    }
}
