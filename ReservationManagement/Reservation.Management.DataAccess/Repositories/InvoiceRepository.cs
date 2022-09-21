using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class InvoiceRepository : GenericRepository<Entities.Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
