
namespace Hotel.Management.Service.Interfaces
{
    public interface IHotelService
    {
        Task<int> CreateHotelAsync(Model.Hotel hotel);
        Task UpdateHotelAsync(Model.Hotel hotel);
        Task DeleteHotelAsync(int hotelId);
        Task<Model.Hotel?> GetHotelByIdAsync(int hotelId);
        Task<Model.HotelDetails?> GetHotelDetailsByIdAsync(int hotelId);
        Task<List<Model.Hotel>> GetHotelsByCityIdAsync(int cityId);
        Task<List<Model.Hotel>> GetHotelsByStateCountryAsync(string state, string country);
        Task<List<Model.Hotel>> GetHotelsByCityStateCountryAsync(string city, string state, string country);
    }
}
