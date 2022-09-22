using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace Reservation.Management.DataAccess.Entities
{
    public partial class Reservation: IEntity
    {
        public Reservation()
        {
            Invoices = new HashSet<Invoice>();
            ReservationHistories = new HashSet<ReservationHistory>();
            RoomReservations = new HashSet<RoomReservation>();
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public string? Observations { get; set; }
        public int Status { get; set; }
        public int? PaymentMethodType { get; set; }
        public string? PaymentMethodInfo { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<ReservationHistory> ReservationHistories { get; set; }
        public virtual ICollection<RoomReservation> RoomReservations { get; set; }
    }
}
