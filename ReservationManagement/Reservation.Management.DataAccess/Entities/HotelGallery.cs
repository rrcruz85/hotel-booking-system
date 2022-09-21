﻿using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class HotelGallery: IEntity
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string BlobImageUri { get; set; } = null!;
        public string? Description { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
    }
}
