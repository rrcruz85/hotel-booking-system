using Hotel.Booking.Common.Contract.EfImpl;
using Hotel.Management.DataAccess.Interfaces;

namespace Hotel.Management.DataAccess.Repositories
{
    public class HotelServiceRepository : GenericRepository<Entities.HotelService>, IHotelServiceRepository
    {
        public HotelServiceRepository(HotelManagementContext context): base(context)
        {
        }
    }
}
