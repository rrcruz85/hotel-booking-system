using Hotel.Booking.Common.Contract.EfImpl;
using Hotel.Management.DataAccess.Interfaces;

namespace Hotel.Management.DataAccess.Repositories
{
    public class HotelRepository : GenericRepository<Entities.Hotel>, IHotelRepository
    {
        public HotelRepository(HotelManagementContext context): base(context)
        {
        }
    }
}
