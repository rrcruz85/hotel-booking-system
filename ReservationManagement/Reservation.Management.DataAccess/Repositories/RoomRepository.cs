using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Entities;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
