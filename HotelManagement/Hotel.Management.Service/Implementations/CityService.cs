using Hotel.Booking.Common.Constant;
using Hotel.Booking.Common.Contract.Exceptions;
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
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfigurationView _config;
        private readonly string TopicName = "CityTopicName";

        public CityService(ICityRepository cityRepository, IMessagingEngine messagingEngine, IConfigurationView config)
        {
            _cityRepository = cityRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }

        public async Task<int> CreateCityAsync(City city)
        {
            if (await _cityRepository.AnyAsync(c => c.Name == city.Name && c.State == city.State && c.Country == city.Country))
            {
                throw new InvalidValueException($"City name can not be duplicated");
            }

            var cityId = await _cityRepository.AddAsync(city.ToNewEntity());
            city.Id = cityId;
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)CityEventType.Created, city);
            return cityId;
        }

        public async Task DeleteCityAsync(int cityId)
        {
            var city = await _cityRepository.SingleOrDefaultAsync(c => c.Id == cityId);
            if (city == null)
            {
                throw new InvalidValueException($"City {cityId} does not exist");
            }
            await _cityRepository.DeleteAsync(city);
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)CityEventType.Deleted, cityId);
        }

        public async Task<List<City>> GetCitiesByCountryAsync(string country)
        {
            var cites = await _cityRepository.WhereAsync(c => c.Country == country);
            return cites.Select(c => c.ToModel()).OrderBy(c => c.Name).ToList();
        }

        public async Task<List<City>> GetCitiesByNameAsync(string name)
        {
            var cites = await _cityRepository.WhereAsync(c => c.Name == name);
            return cites.Select(c => c.ToModel()).OrderBy(c => c.Name).ToList();
        }

        public async Task<List<City>> GetCitiesByStateCountryAsync(string state, string country)
        {
            var cites = await _cityRepository.WhereAsync(c => c.State == state && c.Country == country);
            return cites.Select(c => c.ToModel()).OrderBy(c => c.Name).ToList();
        }

        public async Task<City?> GetCityByAsync(int cityId)
        {
            var city = await _cityRepository.SingleOrDefaultAsync(c => c.Id == cityId);
            return city?.ToModel();
        }

        public async Task UpdateCityAsync(City city)
        {
            var entity = await _cityRepository.SingleOrDefaultAsync(c => c.Id == city.Id);
            if (entity == null)
            {
                throw new InvalidValueException($"City {city.Id} does not exist");
            }
            if (await _cityRepository.AnyAsync(c => c.Id != city.Id && c.Name == city.Name && c.State == city.State && c.Country == city.Country))
            {
                throw new InvalidValueException($"City name can not be duplicated");
            }
            await _cityRepository.UpdateAsync(city.ToEntity());
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)CityEventType.Updated, city);
        }
    }
}
