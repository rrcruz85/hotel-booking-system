using Hotel.Booking.Common.Contract.DataAccess;

namespace Hotel.Management.DataAccess.Interfaces
{
    public interface IHotelRepository : IRepository<Entities.Hotel>
    {
        Task<Entities.Hotel?> GetHotelWithDetails(int hotelId);
    }
}
