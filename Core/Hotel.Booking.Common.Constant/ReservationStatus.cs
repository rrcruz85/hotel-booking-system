using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Booking.Common.Constant
{
    public enum ReservationStatus
    {
        Booked = 1,
        OnGoing,
        Payed,
        Canceled,
        Deleted
    }

    public enum InvoiceStatus
    {
        Issued = 1,
        Payed,
        Canceled,
    }
}
