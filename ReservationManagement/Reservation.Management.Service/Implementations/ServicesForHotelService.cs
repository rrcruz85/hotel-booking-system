using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Service.Translators;

namespace Reservation.Management.Service.Implementations
{
    public class ServicesForHotelService : IServicesForHotelService
    {
        private readonly IHotelServiceRepository _hotelServiceRepository;
        
        public ServicesForHotelService(IHotelServiceRepository hotelServiceRepository)
        {
            _hotelServiceRepository = hotelServiceRepository;
        }

        public async Task<int> CreateHotelServiceAsync(Model.HotelService hotelService)
        {
            if (await _hotelServiceRepository.AnyAsync(c => c.Name == hotelService.Name && c.HotelId == hotelService.HotelId))
            {
                throw new ArgumentException($"Hotel service name can not be duplicated");
            }

            return await _hotelServiceRepository.AddAsync(hotelService.ToEntity());
        }

        public async Task DeleteHotelServiceAsync(int hotelServiceId)
        {
            var entity = await _hotelServiceRepository.SingleOrDefaultAsync(c => c.Id == hotelServiceId);
            if (entity == null)
            {
                throw new ArgumentException($"Hotel service {hotelServiceId} does not exist");
            }
            await _hotelServiceRepository.DeleteAsync(entity);
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
        }
    }
}
