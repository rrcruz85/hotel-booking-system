
namespace Reservation.Management.Service.Interfaces
{
    public interface IReservationService
    {
        Task<int> CreateReservationAsync(Model.CreateUpdateReservation reservation);
        Task UpdateReservationAsync(Model.CreateUpdateReservation reservation);
        Task UpdateReservationStatusAsync(int reservationId, int status, int userId, string observations);
        Task DeleteReservationAsync(int reservationId, int userId);
        Task<Model.Reservation?> GetReservationByIdAsync(int reservationId);
        Task<Model.ReservationDetails?> GetReservationDetailsByIdAsync(int reservationId);
        Task<List<Model.ReservationDetails>> GetReservationsByHotelIdAsync(int hotelId);
        Task<List<Model.ReservationDetails>> GetReservationsByUserIdAsync(int userId);
        Task<List<Model.ReservationDetails>> GetReservationsByHotelIdAndDatesAsync(int hotelId, DateTime startDate, DateTime endDate);
    }
}
