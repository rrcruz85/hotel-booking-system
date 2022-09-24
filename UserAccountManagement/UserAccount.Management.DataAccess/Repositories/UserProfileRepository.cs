using Hotel.Booking.Common.Contract.EfImpl;
using UserAccount.Management.DataAccess.Interfaces;

namespace UserAccount.Management.DataAccess.Repositories
{
    public class UserProfileRepository : GenericRepository<Entities.UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(UserAccountManagementContext context): base(context)
        {
        }
    }
}
