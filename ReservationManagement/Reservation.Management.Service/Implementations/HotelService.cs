using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;

namespace Reservation.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        
        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<int> CreateHotelAsync(Model.Hotel hotel)
        {
            if (await _hotelRepository.AnyAsync(c => c.Name == hotel.Name))
            {
                throw new ArgumentException($"Hotel name can not be duplicated");
            }
 
            return await _hotelRepository.AddAsync(hotel.ToEntity());
        }

        public async Task DeleteHotelAsync(int hotelId)
        {
            var hotel = await _hotelRepository.SingleOrDefaultAsync(c => c.Id == hotelId);
            if (hotel == null)
            {
                throw new ArgumentException($"Hotel {hotelId} does not exist");
            }
            await _hotelRepository.DeleteAsync(hotel);
        }

        public async Task<Model.Hotel?> GetHotelByIdAsync(int hotelId)
        {
            var hotel = await _hotelRepository.SingleOrDefaultAsync(c => c.Id == hotelId);
            return hotel?.ToModel();
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
        }
    }
}
