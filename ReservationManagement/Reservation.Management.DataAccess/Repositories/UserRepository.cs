using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class UserContactInfoRepository : GenericRepository<Entities.UserContactInfo>, IUserContactInfoRepository
    {
        public UserContactInfoRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
