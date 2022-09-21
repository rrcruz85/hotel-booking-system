using Hotel.Booking.Common.Contract.EfImpl;
using Hotel.Management.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Management.DataAccess.Repositories
{
    public class HotelRepository : GenericRepository<Entities.Hotel>, IHotelRepository
    {
        public HotelRepository(HotelManagementContext context): base(context)
        {
        }

        public async Task<Entities.Hotel?> GetHotelWithDetails(int hotelId)
        {
            return await DbSet
                .Include(x => x.HotelContactInfos)
                .Include(x => x.HotelGalleries)
                .Include(x => x.City)
                .Include(x => x.HotelCategoryRelations)
                .Include(x => x.HotelFacilities)
                .Include(x => x.HotelServices)
                .Include(x => x.Rooms)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == hotelId);
        }
    }
}
