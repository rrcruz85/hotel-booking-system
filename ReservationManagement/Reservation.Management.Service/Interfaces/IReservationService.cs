
namespace Reservation.Management.Service.Interfaces
{
    public interface IReservationService
    {
        Task<int> CreateReservationAsync(Model.CreateReservation reservation);
        Task UpdateReservationAsync(Model.Reservation room);
        Task UpdateReservationStatusAsync(int reservationId, int status, string observations);
        Task DeleteReservationAsync(int reservationId);
        Task<Model.Reservation?> GetReservationByIdAsync(int reservationId);
        Task<Model.Reservation?> GetReservationDetailsByIdAsync(int reservationId);
        Task<List<Model.Reservation>> GetReservationsByHotelIdAsync(int hotelId);
        Task<List<Model.Reservation>> GetReservationsByUserIdAsync(int userId);        
    }
}
