
namespace Hotel.Management.Service.Interfaces
{
    public interface IHotelCategoryService
    {
        Task<int> CreateHotelCategoryAsync(Model.HotelCategory category);
        Task UpdateHotelCategoryAsync(Model.HotelCategory category);
        Task DeleteHotelCategoryAsync(int hotelCategoryId);
        Task<Model.HotelCategory?> GetHotelCategoryByAsync(int hotelCategoryId);
        Task<List<Model.HotelCategory>> GetHotelCategoriesByHotelAsync(int hotelId);         
    }
}
