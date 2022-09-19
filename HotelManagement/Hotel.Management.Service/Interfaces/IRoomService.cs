
namespace Hotel.Management.Service.Interfaces
{
    public interface IRoomService
    {
        Task<int> CreateRoomAsync(Model.Room room);
        Task UpdateRoomAsync(Model.Room room);
        Task UpdateRoomStatusAsync(int roomId, int status);
        Task DeleteRoomAsync(int roomId);
        Task<Model.Room?> GetRoomByAsync(int roomId);
        Task<List<Model.Room>> GetAllRoomsByHotelIdAsync(int hotelId);
        Task<Model.Room?> GetRoomByNumberAsync(int hotelId, int number);
        Task<List<Model.Room>> GetRoomsByStatusAndHotelAsync(int hotelId, int status);       
    }
}
