using Hotel.Booking.Common.Contract.EfImpl;
using UserAccount.Management.DataAccess.Interfaces;

namespace UserAccount.Management.DataAccess.Repositories
{
    public class UserContactInfoRepository : GenericRepository<Entities.UserContactInfo>, IUserContactInfoRepository
    {
        public UserContactInfoRepository(UserAccountManagementContext context): base(context)
        {
        }
    }
}
