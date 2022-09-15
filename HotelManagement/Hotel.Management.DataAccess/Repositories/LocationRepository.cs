using Hotel.Booking.Common.Contract.EfImpl;
using Hotel.Management.DataAccess.Interfaces;

namespace Hotel.Management.DataAccess.Repositories
{
    public class LocationRepository : GenericRepository<Entities.Location>, ILocationRepository
    {
        public LocationRepository(HotelManagementContext context): base(context)
        {
        }
    }
}
