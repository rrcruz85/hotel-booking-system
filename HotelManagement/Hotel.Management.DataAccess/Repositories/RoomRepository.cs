using Hotel.Booking.Common.Contract.EfImpl;
using Hotel.Management.DataAccess.Interfaces;

namespace Hotel.Management.DataAccess.Repositories
{
    public class RoomRepository : GenericRepository<Entities.Room>, IRoomRepository
    {
        public RoomRepository(HotelManagementContext context): base(context)
        {
        }
    }
}
