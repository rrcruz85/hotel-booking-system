using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class ReservationHistoryRepository : GenericRepository<Entities.ReservationHistory>, IReservationHistoryRepository
    {
        public ReservationHistoryRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
