
namespace Hotel.Management.Service.Interfaces
{
    public interface IServicesForHotelService
    {
        Task<int> CreateHotelServiceAsync(Model.HotelService hotelService);
        Task UpdateHotelServiceAsync(Model.HotelService hotelService);
        Task DeleteHotelServiceAsync(int hotelServiceId);
        Task<Model.HotelService?> GetHotelServiceByAsync(int hotelServiceId);
        Task<List<Model.HotelService>> GetHotelServiceByHotelAsync(int hotelId);      
    }
}
