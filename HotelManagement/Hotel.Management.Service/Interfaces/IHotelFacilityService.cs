
namespace Hotel.Management.Service.Interfaces
{
    public interface IHotelFacilityService
    {
        Task<int> CreateHotelFacilityAsync(Model.HotelFacility hotelFacility);
        Task UpdateHotelFacilityAsync(Model.HotelFacility hotelFacility);
        Task DeleteHotelFacilityAsync(int hotelFacilityId);
        Task<Model.HotelFacility?> GetHotelFacilityByIdAsync(int hotelFacilityId);
        Task<List<Model.HotelFacility>> GetHotelFacilitiesByHotelAsync(int hotelId);
    }
}
