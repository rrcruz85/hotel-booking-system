using Hotel.Booking.Common.Contract.DataAccess;

namespace Reservation.Management.DataAccess.Interfaces
{
    public interface IReservationRepository : IRepository<Entities.Reservation>
    {
        Task<Entities.Reservation?> GetWithRelationsAsync(int id);
    }
}
