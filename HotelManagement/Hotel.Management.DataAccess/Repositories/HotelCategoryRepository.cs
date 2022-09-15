using Hotel.Booking.Common.Contract.EfImpl;
using Hotel.Management.DataAccess.Interfaces;

namespace Hotel.Management.DataAccess.Repositories
{
    public class HotelCategoryRepository : GenericRepository<Entities.HotelCategory>, IHotelCategoryRepository
    {
        public HotelCategoryRepository(HotelManagementContext context): base(context)
        {
        }
    }
}
