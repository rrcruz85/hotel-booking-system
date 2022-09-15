using Hotel.Booking.Common.Contract.EfImpl;
using Hotel.Management.DataAccess.Interfaces;

namespace Hotel.Management.DataAccess.Repositories
{
    public class HotelContactInfoRepository : GenericRepository<Entities.HotelContactInfo>, IHotelContactInfoRepository
    {
        public HotelContactInfoRepository(HotelManagementContext context): base(context)
        {
        }
    }
}
