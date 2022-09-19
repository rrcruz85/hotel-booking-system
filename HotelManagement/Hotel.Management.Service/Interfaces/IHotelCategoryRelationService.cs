namespace Hotel.Management.Service.Interfaces
{
    public interface IHotelCategoryRelationService
    {
        Task<int> CreateHotelCategoryRelationAsync(int hotelId, int categotyId);
        Task DeleteHotelCategoryRelationAsync(int hotelCategoryRelationId);
        Task DeleteHotelCategoryRelationAsync(int hotelId, int categotyId);
    }
}