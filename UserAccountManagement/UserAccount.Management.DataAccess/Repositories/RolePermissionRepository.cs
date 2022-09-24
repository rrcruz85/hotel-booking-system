using Hotel.Booking.Common.Contract.EfImpl;
using UserAccount.Management.DataAccess.Interfaces;

namespace UserAccount.Management.DataAccess.Repositories
{
    public class RolePermissionRepository : GenericRepository<Entities.RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(UserAccountManagementContext context): base(context)
        {
        }
    }
}
