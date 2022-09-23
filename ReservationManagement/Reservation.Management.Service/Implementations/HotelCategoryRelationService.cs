using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Service.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Reservation.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class HotelCategoryRelationService : IHotelCategoryRelationService
    {
        private readonly IHotelCategoryRelationRepository _hotelCategoryRelationRepository;
       
        public HotelCategoryRelationService(IHotelCategoryRelationRepository hotelCategoryRelationRepository)
        {            
            _hotelCategoryRelationRepository = hotelCategoryRelationRepository;            
        }

        public async Task<int> CreateHotelCategoryRelationAsync(int hotelId, int categotyId)
        {
            var entity = new DataAccess.Entities.HotelCategoryRelation { HotelId = hotelId, HotelCategoryId = categotyId };
            return await _hotelCategoryRelationRepository.AddAsync(entity);
        }

        public async Task DeleteHotelCategoryRelationAsync(int hotelCategoryRelationId)
        {
            var relation = await _hotelCategoryRelationRepository.SingleOrDefaultAsync(c => c.Id == hotelCategoryRelationId);
            if (relation == null)
            {
                throw new ArgumentException($"Hotel category relation {hotelCategoryRelationId} does not exist");
            }
            await _hotelCategoryRelationRepository.DeleteAsync(relation);
        }

        public async Task DeleteHotelCategoryRelationAsync(int hotelId, int categotyId)
        {
            var entity = await _hotelCategoryRelationRepository.FirstOrDefaultAsync(c => c.HotelId == hotelId && c.HotelCategoryId == categotyId);
            if (entity != null)
            {
                await _hotelCategoryRelationRepository.DeleteAsync(entity);
            }
        }

    }
}
