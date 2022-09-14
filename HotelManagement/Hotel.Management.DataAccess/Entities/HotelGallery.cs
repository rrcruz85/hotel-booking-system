using Hotel.Booking.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Hotel.Management.DataAccess.Entities
{
    public partial class HotelGallery : IEntity
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string BlobImageUri { get; set; } = null!;
        public string? Description { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
    }
}
