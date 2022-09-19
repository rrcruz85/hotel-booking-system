using Hotel.Booking.Common.Contract.EfImpl;
using Hotel.Management.DataAccess.Interfaces;

namespace Hotel.Management.DataAccess.Repositories
{
    public class HotelCategoryRelationRepository : GenericRepository<Entities.HotelCategoryRelation>, IHotelCategoryRelationRepository
    {
        public HotelCategoryRelationRepository(HotelManagementContext context): base(context)
        {
        }
    }
}
