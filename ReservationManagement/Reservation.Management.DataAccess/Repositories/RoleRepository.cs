using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class RoleRepository : GenericRepository<Entities.Role>, IRoleRepository
    {
        public RoleRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
