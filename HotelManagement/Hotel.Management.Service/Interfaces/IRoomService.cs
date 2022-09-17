
using System.Linq.Expressions;

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
        Task<Model.Room?> GetRoomByNumberAsync(int hotelId, string number);
        Task<List<Model.Room>> GetRoomsByStatusAndHotelAsync(int status, DateTime startDate, DateTime endDate, int? hotelId);
        Task<List<Model.Room>> GetRoomsByCriteriaAsync(Expression<Func<Model.Room, bool>> filter);
    }
}
