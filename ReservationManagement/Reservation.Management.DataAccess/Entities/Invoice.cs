using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class Invoice: IEntity
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public decimal Total { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? CanceledAt { get; set; }
        public int Status { get; set; }
        public string? Observations { get; set; }

        public virtual Reservation Reservation { get; set; } = null!;
    }
}
