using Hotel.Booking.Common.Contract.EfImpl;
using UserAccount.Management.DataAccess.Interfaces;

namespace UserAccount.Management.DataAccess.Repositories
{
    public class RoleRepository : GenericRepository<Entities.Role>, IRoleRepository
    {
        public RoleRepository(UserAccountManagementContext context): base(context)
        {
        }
    }
}
