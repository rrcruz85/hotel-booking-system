using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Service.Translators;

namespace Reservation.Management.Service.Implementations
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<int> CreateCityAsync(City city)
        {
            return await _cityRepository.AddAsync(city.ToEntity());
        }

        public async Task DeleteCityAsync(int cityId)
        {
            var city = await _cityRepository.SingleOrDefaultAsync(c => c.Id == cityId);
            if (city == null)
            {
                throw new ArgumentException($"City {cityId} does not exist");
            }
            await _cityRepository.DeleteAsync(city);
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
                throw new ArgumentException($"City {city.Id} does not exist");
            }
            if (await _cityRepository.AnyAsync(c => c.Id != city.Id && c.Name == city.Name && c.State == city.Name && c.Country == city.Country))
            {
                throw new ArgumentException($"City name can not be duplicated");
            }
            await _cityRepository.UpdateAsync(city.ToEntity());
        }
    }
}
