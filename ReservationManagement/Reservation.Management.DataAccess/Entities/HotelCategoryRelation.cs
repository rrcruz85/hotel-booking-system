using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class HotelCategoryRelation: IEntity
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int HotelCategoryId { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
        public virtual HotelCategory HotelCategory { get; set; } = null!;
    }
}
