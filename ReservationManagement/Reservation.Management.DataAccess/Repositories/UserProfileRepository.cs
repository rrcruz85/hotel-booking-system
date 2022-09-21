using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class UserProfileRepository : GenericRepository<Entities.UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
