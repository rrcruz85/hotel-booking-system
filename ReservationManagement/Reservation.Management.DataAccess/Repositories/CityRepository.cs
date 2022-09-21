using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class CityRepository : GenericRepository<Entities.City>, ICityRepository
    {
        public CityRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
