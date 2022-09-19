
namespace Hotel.Management.Service.Interfaces
{
    public interface IHotelContactInfoService
    {
        Task<int> CreateHotelContactInfoAsync(Model.HotelContactInfo contactInfo);
        Task UpdateHotelContactInfoAsync(Model.HotelContactInfo contactInfo);
        Task DeleteHotelContactInfoAsync(int hotelContactInfoId);
        Task<Model.HotelContactInfo?> GetHotelContactInfoByAsync(int hotelContactInfoId);
        Task<List<Model.HotelContactInfo>> GetHotelContactInfoByHotelAsync(int hotelId);
        Task<Model.HotelContactInfo?> GetHotelContactInfoByHotelAndTypeAsync(int hotelId, int type);
    }
}
