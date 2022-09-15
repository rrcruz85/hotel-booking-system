using Hotel.Booking.Common.Contract.EfImpl;
using Hotel.Management.DataAccess.Interfaces;

namespace Hotel.Management.DataAccess.Repositories
{
    public class HotelFacilityRepository : GenericRepository<Entities.HotelFacility>, IHotelFacilityRepository
    {
        public HotelFacilityRepository(HotelManagementContext context): base(context)
        {
        }
    }
}
