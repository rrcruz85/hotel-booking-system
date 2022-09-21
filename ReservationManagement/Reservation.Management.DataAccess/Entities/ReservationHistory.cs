using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class ReservationHistory: IEntity
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? Description { get; set; }

        public virtual Reservation Reservation { get; set; } = null!;
    }
}
