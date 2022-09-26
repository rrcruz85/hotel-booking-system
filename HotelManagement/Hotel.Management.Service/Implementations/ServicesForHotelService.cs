using Hotel.Booking.Common.Constant;
using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Booking.Common.Contract.Services;
using Hotel.Management.DataAccess.Interfaces;
using Hotel.Management.Service.Interfaces;
using Hotel.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;

namespace Hotel.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class ServicesForHotelService : IServicesForHotelService
    {
        private readonly IHotelServiceRepository _hotelServiceRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfigurationView _config;
        private readonly string TopicName = "HotelServiceTopicName";

        public ServicesForHotelService(
            IHotelServiceRepository hotelServiceRepository, 
            IMessagingEngine messagingEngine, 
            IConfigurationView config)
        {
            _hotelServiceRepository = hotelServiceRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }

        public async Task<int> CreateHotelServiceAsync(Model.HotelService hotelService)
        {
            if (await _hotelServiceRepository.AnyAsync(c => c.Name == hotelService.Name && c.HotelId == hotelService.HotelId))
            {
                throw new ArgumentException($"Hotel service name can not be duplicated");
            }

            var serviceId = await _hotelServiceRepository.AddAsync(hotelService.ToNewEntity());
            hotelService.Id = serviceId;
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)HotelServiceEventType.Created, hotelService);
            return serviceId;
        }

        public async Task DeleteHotelServiceAsync(int hotelServiceId)
        {
            var entity = await _hotelServiceRepository.SingleOrDefaultAsync(c => c.Id == hotelServiceId);
            if (entity == null)
            {
                throw new ArgumentException($"Hotel service {hotelServiceId} does not exist");
            }
            await _hotelServiceRepository.DeleteAsync(entity);
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)HotelServiceEventType.Deleted, hotelServiceId);
        }

        public async Task<Model.HotelService?> GetHotelServiceByAsync(int hotelServiceId)
        {
            var service = await _hotelServiceRepository.SingleOrDefaultAsync(c => c.Id == hotelServiceId);
            return service?.ToModel();
        }

        public async Task<List<Model.HotelService>> GetHotelServiceByHotelAsync(int hotelId)
        {
            var facilities = await _hotelServiceRepository.WhereAsync(c => c.HotelId == hotelId);
            return facilities.Select(f => f.ToModel()).ToList();
        }

        public async Task UpdateHotelServiceAsync(Model.HotelService hotelService)
        {
            var entity = await _hotelServiceRepository.SingleOrDefaultAsync(c => c.Id == hotelService.Id);
            if (entity == null)
            {
                throw new ArgumentException($"Hotel service {hotelService.Id} does not exist");
            }
            if (await _hotelServiceRepository.AnyAsync(c => c.Id != hotelService.Id && c.HotelId == hotelService.HotelId && c.Name == hotelService.Name))
            {
                throw new ArgumentException($"Hotel service name can not be duplicated");
            }
            await _hotelServiceRepository.UpdateAsync(hotelService.ToEntity());
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)HotelServiceEventType.Updated, hotelService);
        }
    }
}
