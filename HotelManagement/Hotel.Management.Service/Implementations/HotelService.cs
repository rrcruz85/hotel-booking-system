using Hotel.Booking.Common.Constant;
using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Management.DataAccess.Interfaces;
using Hotel.Management.Service.Interfaces;
using Hotel.Management.Service.Translators;
using Microsoft.Extensions.Configuration;

namespace Hotel.Management.Service.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfiguration _config;
        private readonly string TopicName = "HotelTopicName";

        public HotelService(
            ICityRepository cityRepository,
            IHotelRepository hotelRepository, 
            IMessagingEngine messagingEngine, 
            IConfiguration config)
        {
            _cityRepository = cityRepository;
            _hotelRepository = hotelRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }

        public async Task<int> CreateHotelAsync(Model.Hotel hotel)
        {
            if (await _hotelRepository.AnyAsync(c => c.Name == hotel.Name))
            {
                throw new ArgumentException($"Hotel name can not be duplicated");
            }

            if (!await _cityRepository.AnyAsync(x => x.Id == hotel.CityId))
            {
                throw new ArgumentException($"City does not exist");
            }

            var hotelId = await _hotelRepository.AddAsync(hotel.ToNewEntity());
            hotel.Id = hotelId;
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelEventType.Created, hotel);
            return hotelId;
        }

        public async Task DeleteHotelAsync(int hotelId)
        {
            var hotel = await _hotelRepository.SingleOrDefaultAsync(c => c.Id == hotelId);
            if (hotel == null)
            {
                throw new ArgumentException($"Hotel {hotelId} does not exist");
            }
            await _hotelRepository.DeleteAsync(hotel);
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelEventType.Deleted, hotelId);
        }

        public async Task<Model.Hotel?> GetHotelByIdAsync(int hotelId)
        {
            var hotel = await _hotelRepository.SingleOrDefaultAsync(c => c.Id == hotelId);
            return hotel?.ToModel();
        }

        public async Task<Model.HotelDetails?> GetHotelDetailsByIdAsync(int hotelId)
        {
            var hotel = await _hotelRepository.GetHotelWithDetails(hotelId);
            return hotel?.ToDetailedModel();
        }

        public async Task<List<Model.Hotel>> GetHotelsByCityIdAsync(int cityId)
        {
            var hotels = await _hotelRepository.WhereAsync(c => c.CityId == cityId);
            return hotels.Select(f => f.ToModel()).ToList();
        }

        public async Task<List<Model.Hotel>> GetHotelsByCityStateCountryAsync(string city, string state, string country)
        {
            var hotels = await _hotelRepository.WhereAsync(c => c.City.Name == city && c.City.State == state && c.City.Country == country);
            return hotels.Select(f => f.ToModel()).ToList();
        }

        public async Task<List<Model.Hotel>> GetHotelsByStateCountryAsync(string state, string country)
        {
            var hotels = await _hotelRepository.WhereAsync(c => c.City.State == state && c.City.Country == country);
            return hotels.Select(f => f.ToModel()).ToList();
        }

        public async Task UpdateHotelAsync(Model.Hotel hotel)
        {
            var entity = await _hotelRepository.SingleOrDefaultAsync(c => c.Id == hotel.Id);
            if (entity == null)
            {
                throw new ArgumentException($"Hotel {hotel.Id} does not exist");
            }
            if (await _hotelRepository.AnyAsync(h => h.Id != hotel.Id && h.Name == hotel.Name))
            {
                throw new ArgumentException($"Hotel name can not be duplicated");
            }
            await _hotelRepository.UpdateAsync(hotel.ToEntity());
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelEventType.Updated, hotel);
        }
    }
}
