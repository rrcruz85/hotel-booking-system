using Hotel.Booking.Common.Contract.EfImpl;
using UserAccount.Management.DataAccess.Interfaces;

namespace UserAccount.Management.DataAccess.Repositories
{
    public class CityRepository : GenericRepository<Entities.City>, ICityRepository
    {
        public CityRepository(UserAccountManagementContext context): base(context)
        {
        }
    }
}
