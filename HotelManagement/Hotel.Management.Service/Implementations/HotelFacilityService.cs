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
    public class HotelFacilityService : IHotelFacilityService
    {
        private readonly IHotelFacilityRepository _hotelFacilityRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfigurationView _config;
        private readonly string TopicName = "HotelFacilityTopicName";

        public HotelFacilityService(
            IHotelFacilityRepository hotelFacilityRepository, 
            IMessagingEngine messagingEngine,
            IConfigurationView config)
        {
            _hotelFacilityRepository = hotelFacilityRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }

        public async Task<int> CreateHotelFacilityAsync(HotelFacility hotelFacility)
        {
            if (await _hotelFacilityRepository.AnyAsync(c => c.Name == hotelFacility.Name && c.HotelId == hotelFacility.HotelId))
            {
                throw new ArgumentException($"Hotel facility name can not be duplicated");
            }

            var facilityId = await _hotelFacilityRepository.AddAsync(hotelFacility.ToNewEntity());
            hotelFacility.Id = facilityId;
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)HotelFacilityEventType.Created, hotelFacility);
            return facilityId;
        }

        public async Task DeleteHotelFacilityAsync(int hotelFacilityId)
        {
            var facility = await _hotelFacilityRepository.SingleOrDefaultAsync(c => c.Id == hotelFacilityId);
            if (facility == null)
            {
                throw new ArgumentException($"Hotel facility {hotelFacilityId} does not exist");
            }
            await _hotelFacilityRepository.DeleteAsync(facility);
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)HotelFacilityEventType.Deleted, hotelFacilityId);

        }         

        public async Task<List<HotelFacility>> GetHotelFacilitiesByHotelAsync(int hotelId)
        {
            var facilities = await _hotelFacilityRepository.WhereAsync(c => c.HotelId == hotelId);
            return facilities.Select(f => f.ToModel()).ToList();
        }

        public async Task<HotelFacility?> GetHotelFacilityByIdAsync(int hotelFacilityId)
        {
            var facility = await _hotelFacilityRepository.FirstOrDefaultAsync(c => c.Id == hotelFacilityId);
            return facility?.ToModel();
        }

        public async Task UpdateHotelFacilityAsync(HotelFacility hotelFacility)
        {
            var entity = await _hotelFacilityRepository.SingleOrDefaultAsync(c => c.Id == hotelFacility.Id);
            if (entity == null)
            {
                throw new ArgumentException($"Hotel facility {hotelFacility.Id} does not exist");
            }
            if (await _hotelFacilityRepository.AnyAsync(c => c.Id != hotelFacility.Id && c.HotelId == hotelFacility.HotelId && c.Name == hotelFacility.Name))
            {
                throw new ArgumentException($"Hotel facility name can not be duplicated");
            }
            await _hotelFacilityRepository.UpdateAsync(hotelFacility.ToEntity());
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)HotelFacilityEventType.Updated, hotelFacility);

        }
    }
}
