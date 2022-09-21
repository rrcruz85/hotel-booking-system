using Hotel.Booking.Common.Contract.EfImpl;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class RoomReservationRepository : GenericRepository<Entities.RoomReservation>, IRoomReservationRepository
    {
        public RoomReservationRepository(ReservationManagementContext context): base(context)
        {
        }
    }
}
