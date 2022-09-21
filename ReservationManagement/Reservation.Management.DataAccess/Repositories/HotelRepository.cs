using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class HotelRepository : GenericRepository<Entities.Hotel>, IHotelRepository
    {
        public HotelRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
