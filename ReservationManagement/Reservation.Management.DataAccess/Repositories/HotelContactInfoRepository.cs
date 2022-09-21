using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class HotelContactInfoRepository : GenericRepository<Entities.HotelContactInfo>, IHotelContactInfoRepository
    {
        public HotelContactInfoRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
