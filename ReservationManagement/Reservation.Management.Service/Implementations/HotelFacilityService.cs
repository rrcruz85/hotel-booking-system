using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;

namespace Reservation.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class HotelFacilityService : IHotelFacilityService
    {
        private readonly IHotelFacilityRepository _hotelFacilityRepository;
        
        public HotelFacilityService(IHotelFacilityRepository hotelFacilityRepository)
        {
            _hotelFacilityRepository = hotelFacilityRepository;
        }

        public async Task<int> CreateHotelFacilityAsync(HotelFacility hotelFacility)
        {
            if (await _hotelFacilityRepository.AnyAsync(c => c.Name == hotelFacility.Name && c.HotelId == hotelFacility.HotelId))
            {
                throw new ArgumentException($"Hotel facility name can not be duplicated");
            }

            return await _hotelFacilityRepository.AddAsync(hotelFacility.ToEntity());
        }

        public async Task DeleteHotelFacilityAsync(int hotelFacilityId)
        {
            var facility = await _hotelFacilityRepository.SingleOrDefaultAsync(c => c.Id == hotelFacilityId);
            if (facility == null)
            {
                throw new ArgumentException($"Hotel facility {hotelFacilityId} does not exist");
            }
            await _hotelFacilityRepository.DeleteAsync(facility);
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
        }
    }
}
