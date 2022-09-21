using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class HotelFacilityRepository : GenericRepository<Entities.HotelFacility>, IHotelFacilityRepository
    {
        public HotelFacilityRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
