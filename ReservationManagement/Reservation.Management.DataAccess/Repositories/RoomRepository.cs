using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class RoomRepository : GenericRepository<Entities.Room>, IRoomRepository
    {
        public RoomRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
