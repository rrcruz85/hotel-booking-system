using Hotel.Booking.Common.Contract.EfImpl;
using UserAccount.Management.DataAccess.Interfaces;

namespace UserAccount.Management.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<Entities.User>, IUserRepository
    {
        public UserRepository(UserAccountManagementContext context): base(context)
        {
        }
    }
}
