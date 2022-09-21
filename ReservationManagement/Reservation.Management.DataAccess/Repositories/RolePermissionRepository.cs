using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class RolePermissionRepository : GenericRepository<Entities.RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
