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

        public async Task<List<Entities.Reservation>> GetByHotelIdWithRelationsAsync(int hotelId)
        {
            return await DbSet.Include(x => x.RoomReservations)
                .Include(x => x.Invoices)
                .Include(x => x.ReservationHistories)
                .Include(x => x.User)
                .Include(x => x.User.UserProfiles)
                .AsNoTracking()
                .Where(x => x.RoomReservations.Any(r => r.Room.HotelId == hotelId))
                .ToListAsync();
        }

        public async Task<List<Entities.Reservation>> GetByUserIdWithRelationsAsync(int userId)
        {
            return await DbSet.Include(x => x.RoomReservations)
                .Include(x => x.Invoices)
                .Include(x => x.ReservationHistories)
                .Include(x => x.User)
                .Include(x => x.User.UserProfiles)
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Entities.Reservation>> GetReservationsByHotelAndDatesAsync(int hotelId, DateTime startDate, DateTime endDate)
        {
            return await DbSet.Include(x => x.RoomReservations)
                .Include(x => x.Invoices)
                .Include(x => x.ReservationHistories)
                .Include(x => x.User)
                .Include(x => x.User.UserProfiles)
                .AsNoTracking()
                .Where(x => x.StartDate <= endDate && x.EndDate >= startDate && x.RoomReservations.Any(r => r.Room.HotelId == hotelId))
                .ToListAsync();
        }
    }
}
