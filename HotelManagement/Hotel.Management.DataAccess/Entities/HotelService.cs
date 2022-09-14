﻿using Hotel.Booking.Contract;
using System;
using System.Collections.Generic;

namespace Hotel.Management.DataAccess.Entities
{
    public partial class HotelService : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
    }
}
