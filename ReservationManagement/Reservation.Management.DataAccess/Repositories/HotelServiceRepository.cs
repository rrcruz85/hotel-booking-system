using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class HotelServiceRepository : GenericRepository<Entities.HotelService>, IHotelServiceRepository
    {
        public HotelServiceRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
