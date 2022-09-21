using Hotel.Booking.Common.Constant;
using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Management.DataAccess.Interfaces;
using Hotel.Management.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Hotel.Management.Service.Implementations
{
    public class HotelCategoryRelationService : IHotelCategoryRelationService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelCategoryRepository _hotelCategoryRepository;
        private readonly IHotelCategoryRelationRepository _hotelCategoryRelationRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfiguration _config;
        private readonly string TopicName = "HotelCategoryRelationTopicName";

        public HotelCategoryRelationService(
            IHotelRepository hotelRepository,
            IHotelCategoryRepository hotelCategoryRepository,
            IHotelCategoryRelationRepository hotelCategoryRelationRepository,
            IMessagingEngine messagingEngine,
            IConfiguration config)
        {
            _hotelRepository = hotelRepository;
            _hotelCategoryRepository = hotelCategoryRepository;
            _hotelCategoryRelationRepository = hotelCategoryRelationRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }

        public async Task<int> CreateHotelCategoryRelationAsync(int hotelId, int categotyId)
        {
            if (!await _hotelRepository.AnyAsync(x => x.Id == hotelId))
            {
                throw new ArgumentException($"Hotel does not exist");
            }

            if (!await _hotelCategoryRepository.AnyAsync(x => x.Id == categotyId))
            {
                throw new ArgumentException($"Hotel category does not exist");
            }

            if (await _hotelCategoryRelationRepository.AnyAsync(c => c.HotelId == hotelId && c.HotelCategoryId == categotyId))
            {
                throw new ArgumentException($"Hotel category relation can not be duplicated");
            }

            var entity = new DataAccess.Entities.HotelCategoryRelation { HotelId = hotelId, HotelCategoryId = categotyId };
            var relationId = await _hotelCategoryRelationRepository.AddAsync(entity);
            entity.Id = relationId;
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelCategoryEventType.Created, entity);
            return relationId;
        }

        public async Task DeleteHotelCategoryRelationAsync(int hotelCategoryRelationId)
        {
            var relation = await _hotelCategoryRelationRepository.SingleOrDefaultAsync(c => c.Id == hotelCategoryRelationId);
            if (relation == null)
            {
                throw new ArgumentException($"Hotel category relation {hotelCategoryRelationId} does not exist");
            }
            await _hotelCategoryRelationRepository.DeleteAsync(relation);
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelCategoryEventType.Deleted, hotelCategoryRelationId);
        }

        public async Task DeleteHotelCategoryRelationAsync(int hotelId, int categotyId)
        {
            var entity = await _hotelCategoryRelationRepository.FirstOrDefaultAsync(c => c.HotelId == hotelId && c.HotelCategoryId == categotyId);
            if (entity != null)
            {
                await _hotelCategoryRelationRepository.DeleteAsync(entity);
                await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelCategoryEventType.Deleted, entity.Id);
            }
        }

    }
}
