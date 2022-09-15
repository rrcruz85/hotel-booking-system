using Hotel.Booking.Common.Contract.EfImpl;
using Hotel.Management.DataAccess.Interfaces;

namespace Hotel.Management.DataAccess.Repositories
{
    public class HotelGalleryRepository : GenericRepository<Entities.HotelGallery>, IHotelGalleryRepository
    {
        public HotelGalleryRepository(HotelManagementContext context): base(context)
        {
        }
    }
}
