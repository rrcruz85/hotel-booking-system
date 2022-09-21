using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class Room: IEntity
    {
        public Room()
        {
            RoomReservations = new HashSet<RoomReservation>();
        }

        public int Id { get; set; }
        public int HotelId { get; set; }
        public int Status { get; set; }
        public int Number { get; set; }
        public int MaxCapacity { get; set; }
        public decimal CurrentPrice { get; set; }
        public int Type { get; set; }
        public int? Floor { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
        public virtual ICollection<RoomReservation> RoomReservations { get; set; }
    }
}
