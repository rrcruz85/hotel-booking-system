using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<Entities.User>, IUserRepository
    {
        public UserRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
