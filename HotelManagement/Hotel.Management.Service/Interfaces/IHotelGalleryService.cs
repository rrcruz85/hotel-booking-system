
namespace Hotel.Management.Service.Interfaces
{
    public interface IHotelGalleryService
    {
        Task<int> CreateHotelImageAsync(Model.CreateHotelImage image);
        Task UpdateHotelImageAsync(Model.UpdateHotelImage image);
        Task DeleteHotelImageAsync(int imageId);
        Task<Model.HotelImage?> GetHotelImageByAsync(int imageId);
        Task<List<Model.HotelImage>> GetHotelGalleryAsync(int hotelId);        
    }
}
