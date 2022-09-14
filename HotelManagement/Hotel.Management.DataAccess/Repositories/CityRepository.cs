using Hotel.Booking.Contract.Impl;
using Hotel.Management.DataAccess.Interfaces;

namespace Hotel.Management.DataAccess.Repositories
{
    public class CityRepository : GenericRepository<Entities.City>, ICityRepository
    {
        public CityRepository(HotelManagementContext context): base(context)
        {
        }
    }
}
