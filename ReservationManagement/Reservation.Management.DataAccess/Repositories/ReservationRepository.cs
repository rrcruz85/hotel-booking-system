using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class ReservationRepository : GenericRepository<Entities.Reservation>, IReservationRepository
    {
        public ReservationRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
