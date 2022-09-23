using Hotel.Booking.Common.Contract.DataAccess;

namespace Reservation.Management.DataAccess.Interfaces
{
    public interface IReservationRepository : IRepository<Entities.Reservation>
    {
        Task<Entities.Reservation?> GetWithRelationsAsync(int id);
        Task<List<Entities.Reservation>> GetByHotelIdWithRelationsAsync(int hotelId);
        Task<List<Entities.Reservation>> GetByUserIdWithRelationsAsync(int userId);
        Task<List<Entities.Reservation>> GetReservationsByHotelAndDatesAsync(int hotelId, DateTime startDate, DateTime endDate);
    }
}
