using Hotel.Booking.Common.Constant;
using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Booking.Common.Contract.Services;
using Hotel.Management.DataAccess.Interfaces;
using Hotel.Management.Model;
using Hotel.Management.Service.Interfaces;
using Hotel.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;

namespace Hotel.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class HotelGalleryService : IHotelGalleryService
    {
        private readonly IHotelGalleryRepository _hotelGalleryRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IConfigurationView _config;
        private readonly string TopicName = "HotelGalleryTopicName";
        private readonly string BlobContainer = "hotel";

        public HotelGalleryService(
            IHotelGalleryRepository hotelGalleryRepository, 
            IMessagingEngine messagingEngine,
            IBlobStorageService blobStorageService,
            IConfigurationView config)
        {
            _hotelGalleryRepository = hotelGalleryRepository;
            _messagingEngine = messagingEngine;
            _blobStorageService = blobStorageService;
            _config = config;
        }

        public async Task<int> CreateHotelImageAsync(CreateHotelImage image)
        {
            var absoluteUri = await _blobStorageService.UploadTextAsync(BlobContainer, $"{image.HotelId}-{DateTimeOffset.Now:yyyyMMsshhmm}.png", "image/png", image.BlobImageContent);

            var newImage = new DataAccess.Entities.HotelGallery
            {
                BlobImageUri = absoluteUri,
                Description = image.Description,
                HotelId = image.HotelId
            };

            var imageId = await _hotelGalleryRepository.AddAsync(newImage);
            newImage.Id = imageId;
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)HotelImageEventType.Created, newImage);
            return imageId;
        }

        public async Task DeleteHotelImageAsync(int imageId)
        {
            var image = await _hotelGalleryRepository.SingleOrDefaultAsync(c => c.Id == imageId);
            if (image == null)
            {
                throw new ArgumentException($"Hotel image {imageId} does not exist");
            }

            var fileNameStartIndex = image.BlobImageUri.LastIndexOf('/');
            var fileName = image.BlobImageUri[(fileNameStartIndex + 1)..];
            await _blobStorageService.DeleteBlobByName(BlobContainer, fileName);

            await _hotelGalleryRepository.DeleteAsync(image);
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)HotelImageEventType.Deleted, imageId);
        }

        public async Task<List<HotelImage>> GetHotelGalleryAsync(int hotelId)
        {
            var entities = await _hotelGalleryRepository.WhereAsync(x => x.HotelId == hotelId);
            return entities.Select(e => e.ToModel()).ToList();
        }

        public async Task<HotelImage?> GetHotelImageByAsync(int imageId)
        {
            var entity = await _hotelGalleryRepository.FirstOrDefaultAsync(x => x.Id == imageId);
            return entity?.ToModel();
        }

        public async Task UpdateHotelImageAsync(UpdateHotelImage image)
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

            var fileNameStartIndex = entity.BlobImageUri.LastIndexOf('/');
            var fileName = entity.BlobImageUri[(fileNameStartIndex + 1)..];
            await _blobStorageService.UploadTextAsync(BlobContainer, fileName, "image/png", image.BlobImageContent);

            entity.Description = image.Description;

            await _hotelGalleryRepository.UpdateAsync(entity);
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)HotelImageEventType.Updated, entity);
        }
    }
}
