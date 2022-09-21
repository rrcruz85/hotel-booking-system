using Hotel.Booking.Common.Constant;
using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Management.DataAccess.Interfaces;
using Hotel.Management.Model;
using Hotel.Management.Service.Interfaces;
using Hotel.Management.Service.Translators;
using Microsoft.Extensions.Configuration;

namespace Hotel.Management.Service.Implementations
{
    public class HotelCategoryService : IHotelCategoryService
    {
        private readonly IHotelCategoryRepository _hotelCategoryRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfiguration _config;
        private readonly string TopicName = "HotelCategoryTopicName";

        public HotelCategoryService(
            IHotelCategoryRepository hotelCategoryRepository, 
            IMessagingEngine messagingEngine, 
            IConfiguration config)
        {
            _hotelCategoryRepository = hotelCategoryRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }         

        public async Task<int> CreateHotelCategoryAsync(HotelCategory category)
        {
            if (await _hotelCategoryRepository.AnyAsync(c => c.Name == category.Name))
            {
                throw new ArgumentException($"Hotel category name can not be duplicated");
            }

            var categoryId = await _hotelCategoryRepository.AddAsync(category.ToNewEntity());
            category.Id = categoryId;
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelCategoryEventType.Created, category);
            return categoryId;
        }

        public async Task DeleteHotelCategoryAsync(int hotelCategoryId)
        {
            var city = await _hotelCategoryRepository.SingleOrDefaultAsync(c => c.Id == hotelCategoryId);
            if (city == null)
            {
                throw new ArgumentException($"Hotel category {hotelCategoryId} does not exist");
            }
            await _hotelCategoryRepository.DeleteAsync(city);
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelCategoryEventType.Deleted, hotelCategoryId);
        }

        public async Task<List<HotelCategory>> GetHotelCategoriesByHotelAsync(int hotelId)
        {
            var categories = await _hotelCategoryRepository.WhereAsync(c => c.HotelCategoryRelations.Any(r => r.HotelId == hotelId));
            return categories.Select(c => c.ToModel()).ToList();
        }

        public async Task<HotelCategory?> GetHotelCategoryByAsync(int hotelCategoryId)
        {
            var category = await _hotelCategoryRepository.SingleOrDefaultAsync(c => c.Id == hotelCategoryId);
            return category?.ToModel();
        }

        public async Task UpdateHotelCategoryAsync(HotelCategory category)
        {
            var entity = await _hotelCategoryRepository.SingleOrDefaultAsync(c => c.Id == category.Id);
            if (entity == null)
            {
                throw new ArgumentException($"Hotel Category {category.Id} does not exist");
            }
            if (await _hotelCategoryRepository.AnyAsync(c => c.Id != category.Id && c.Name == category.Name))
            {
                throw new ArgumentException($"Hotel Category name can not be duplicated");
            }
            await _hotelCategoryRepository.UpdateAsync(category.ToEntity());
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelCategoryEventType.Updated, category);
        }
    }
}
