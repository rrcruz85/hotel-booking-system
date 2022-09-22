using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Service.Translators;

namespace Reservation.Management.Service.Implementations
{
    public class HotelGalleryService : IHotelGalleryService
    {
        private readonly IHotelGalleryRepository _hotelGalleryRepository;
        
        public HotelGalleryService(IHotelGalleryRepository hotelGalleryRepository)
        {
            _hotelGalleryRepository = hotelGalleryRepository;
        }

        public async Task<int> CreateHotelImageAsync(HotelGallery image)
        {
            return await _hotelGalleryRepository.AddAsync(image.ToEntity());
        }

        public async Task DeleteHotelImageAsync(int imageId)
        {
            var entity = await _hotelGalleryRepository.SingleOrDefaultAsync(c => c.Id == imageId);
            if (entity == null)
            {
                throw new ArgumentException($"Hotel image {imageId} does not exist");
            }
            await _hotelGalleryRepository.DeleteAsync(entity);
        }

        public async Task<List<HotelGallery>> GetHotelGalleryAsync(int hotelId)
        {
            var entities = await _hotelGalleryRepository.WhereAsync(x => x.HotelId == hotelId);
            return entities.Select(e => e.ToModel()).ToList();
        }

        public async Task<HotelGallery?> GetHotelImageByAsync(int imageId)
        {
            var entity = await _hotelGalleryRepository.FirstOrDefaultAsync(x => x.Id == imageId);
            return entity?.ToModel();
        }

        public async Task UpdateHotelImageAsync(HotelGallery image)
        {
            var entity = await _hotelGalleryRepository.SingleOrDefaultAsync(c => c.Id == image.Id);
            if (entity == null)
            {
                throw new ArgumentException($"Hotel image {image.Id} does not exist");
            }
            if (entity.HotelId != image.HotelId)
            {
                throw new ArgumentException($"The hotel can not be changed");
            }
 
            await _hotelGalleryRepository.UpdateAsync(entity);
        }
    }
}
