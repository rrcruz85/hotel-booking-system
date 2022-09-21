using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class HotelCategoryRelationRepository : GenericRepository<Entities.HotelCategoryRelation>, IHotelCategoryRelationRepository
    {
        public HotelCategoryRelationRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
