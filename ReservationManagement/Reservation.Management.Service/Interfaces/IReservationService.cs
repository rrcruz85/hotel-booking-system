
namespace Reservation.Management.Service.Interfaces
{
    public interface IReservationService
    {
        Task<int> CreateReservationAsync(Model.CreateReservation reservation);
        Task UpdateReservationAsync(Model.Reservation room);
        Task UpdateReservationStatusAsync(int reservationId, int status, string observations);
        Task DeleteReservationAsync(int reservationId);
        Task<Model.Reservation?> GetReservationByIdAsync(int reservationId);
        Task<Model.Reservation?> GetReservationDeatilsByIdAsync(int reservationId);
        Task<List<Model.Reservation>> GetReservationsByHotelIdAsync(int hotelId);
        Task<Model.Room?> GetRoomByNumberAsync(int hotelId, int number);
        Task<List<Model.Room>> GetRoomsByStatusAndHotelAsync(int hotelId, int status);
        Task<List<Model.Room>> GetRoomsByTypeAndHotelAsync(int hotelId, int type);
    }
}
