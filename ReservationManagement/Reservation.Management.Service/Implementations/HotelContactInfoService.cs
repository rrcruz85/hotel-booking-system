using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;

namespace Reservation.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class HotelContactInfoService : IHotelContactInfoService
    {
        private readonly IHotelContactInfoRepository _hotelContactInfoRepository;
      
        public HotelContactInfoService(IHotelContactInfoRepository hotelContactInfoRepository)
        {
            _hotelContactInfoRepository = hotelContactInfoRepository;
        }         

        public async Task<int> CreateHotelContactInfoAsync(HotelContactInfo contactInfo)
        {
            if (await _hotelContactInfoRepository.AnyAsync(c => c.HotelId == contactInfo.HotelId && c.Type == contactInfo.Type))
            {
                throw new ArgumentException($"Hotel Contact Info can not be duplicated");
            }

            return await _hotelContactInfoRepository.AddAsync(contactInfo.ToEntity());
        }

        public async Task DeleteHotelContactInfoAsync(int hotelContactInfoId)
        {
            var contact = await _hotelContactInfoRepository.SingleOrDefaultAsync(c => c.Id == hotelContactInfoId);
            if (contact == null)
            {
                throw new ArgumentException($"Hotel contact info {hotelContactInfoId} does not exist");
            }
            await _hotelContactInfoRepository.DeleteAsync(contact);
        }        

        public async Task<HotelContactInfo?> GetHotelContactInfoByAsync(int hotelContactInfoId)
        {
            var contact = await _hotelContactInfoRepository.SingleOrDefaultAsync(c => c.Id == hotelContactInfoId);
            return contact?.ToModel();
        }

        public async Task<HotelContactInfo?> GetHotelContactInfoByHotelAndTypeAsync(int hotelId, int type)
        {
            var contact = await _hotelContactInfoRepository.SingleOrDefaultAsync(c => c.HotelId == hotelId && c.Type == type);
            return contact?.ToModel();
        }

        public async Task<List<HotelContactInfo>> GetHotelContactInfoByHotelAsync(int hotelId)
        {
            var contacts = await _hotelContactInfoRepository.WhereAsync(c => c.HotelId == hotelId);
            return contacts.Select(c => c.ToModel()).OrderBy(c => c.Type).ToList();
        }
        
        public async Task UpdateHotelContactInfoAsync(HotelContactInfo contactInfo)
        {
            var entity = await _hotelContactInfoRepository.SingleOrDefaultAsync(c => c.Id == contactInfo.Id);
            if (entity == null)
            {
                throw new ArgumentException($"Hotel Contact Info {contactInfo.Id} does not exist");
            }
            if (await _hotelContactInfoRepository.AnyAsync(c => c.Id != contactInfo.Id && c.Type == contactInfo.Type))
            {
                throw new ArgumentException($"Hotel Contact info can not be duplicated");
            }
            await _hotelContactInfoRepository.UpdateAsync(contactInfo.ToEntity());
        }
    }
}
