using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class HotelGalleryRepository : GenericRepository<Entities.HotelGallery>, IHotelGalleryRepository
    {
        public HotelGalleryRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
