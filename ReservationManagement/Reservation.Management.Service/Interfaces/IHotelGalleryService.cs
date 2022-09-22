
namespace Reservation.Management.Service.Interfaces
{
    public interface IHotelGalleryService
    {
        Task<int> CreateHotelImageAsync(Model.HotelGallery image);
        Task UpdateHotelImageAsync(Model.HotelGallery image);
        Task DeleteHotelImageAsync(int imageId);
        Task<Model.HotelGallery?> GetHotelImageByAsync(int imageId);
        Task<List<Model.HotelGallery>> GetHotelGalleryAsync(int hotelId);        
    }
}
