using Hotel.Booking.Common.Contract.EfImpl;
using Microsoft.EntityFrameworkCore;
using Reservation.Management.DataAccess.Interfaces;

namespace Reservation.Management.DataAccess.Repositories
{
    public class ReservationRepository : GenericRepository<Entities.Reservation>, IReservationRepository
    {
        public ReservationRepository(ReservationManagementContext context): base(context)
        {
        }

        public async Task<Entities.Reservation?> GetWithRelationsAsync(int id)
        {
            return await DbSet.Include(x => x.RoomReservations)
                .Include(x => x.Invoices)
                .Include(x => x.ReservationHistories)
                .Include(x => x.User)
                .Include(x => x.User.UserProfiles)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
