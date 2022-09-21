using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class HotelCategoryRepository : GenericRepository<Entities.HotelCategory>, IHotelCategoryRepository
    {
        public HotelCategoryRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
