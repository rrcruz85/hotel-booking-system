using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Service.Translators;

namespace Reservation.Management.Service.Implementations
{
    public class HotelCategoryService : IHotelCategoryService
    {
        private readonly IHotelCategoryRepository _hotelCategoryRepository;
       
        public HotelCategoryService(IHotelCategoryRepository hotelCategoryRepository)
        {
            _hotelCategoryRepository = hotelCategoryRepository;            
        }         

        public async Task<int> CreateHotelCategoryAsync(HotelCategory category)
        {
            if (await _hotelCategoryRepository.AnyAsync(c => c.Name == category.Name))
            {
                throw new ArgumentException($"Hotel category name can not be duplicated");
            }
            return  await _hotelCategoryRepository.AddAsync(category.ToEntity());
        }

        public async Task DeleteHotelCategoryAsync(int hotelCategoryId)
        {
            var city = await _hotelCategoryRepository.SingleOrDefaultAsync(c => c.Id == hotelCategoryId);
            if (city == null)
            {
                throw new ArgumentException($"Hotel category {hotelCategoryId} does not exist");
            }
            await _hotelCategoryRepository.DeleteAsync(city);
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
        }
    }
}
