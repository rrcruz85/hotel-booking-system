using Hotel.Booking.Common.Constant;
using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Management.DataAccess.Interfaces;
using Hotel.Management.Model;
using Hotel.Management.Service.Interfaces;
using Hotel.Management.Service.Translators;
using Microsoft.Extensions.Configuration;

namespace Hotel.Management.Service.Implementations
{
    public class HotelContactInfoService : IHotelContactInfoService
    {
        private readonly IHotelContactInfoRepository _hotelContactInfoRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfiguration _config;
        private readonly string TopicName = "HotelContactInfoTopicName";

        public HotelContactInfoService(
            IHotelContactInfoRepository hotelContactInfoRepository, 
            IMessagingEngine messagingEngine, 
            IConfiguration config)
        {
            _hotelContactInfoRepository = hotelContactInfoRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }         

        public async Task<int> CreateHotelContactInfoAsync(HotelContactInfo contactInfo)
        {
            if (await _hotelContactInfoRepository.AnyAsync(c => c.HotelId == contactInfo.HotelId && c.Type == contactInfo.Type))
            {
                throw new ArgumentException($"Hotel Contact Info can not be duplicated");
            }

            var contactId = await _hotelContactInfoRepository.AddAsync(contactInfo.ToNewEntity());
            contactInfo.Id = contactId;
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelContactInfoEventType.Created, contactInfo);
            return contactId;
        }

        public async Task DeleteHotelContactInfoAsync(int hotelContactInfoId)
        {
            var contact = await _hotelContactInfoRepository.SingleOrDefaultAsync(c => c.Id == hotelContactInfoId);
            if (contact == null)
            {
                throw new ArgumentException($"Hotel contact info {hotelContactInfoId} does not exist");
            }
            await _hotelContactInfoRepository.DeleteAsync(contact);
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelContactInfoEventType.Deleted, hotelContactInfoId);

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

        public IDictionary<int, string> GetHotelContactInfoTypes()
        {
            return new Dictionary<int, string>
            {
                {(int)ContactType.PersonalMobile,  ContactType.PersonalMobile.ToString()},
                {(int)ContactType.WorkMobile,  ContactType.WorkMobile.ToString()},
                {(int)ContactType.HomePhoneNumber,  ContactType.HomePhoneNumber.ToString()},
                {(int)ContactType.WorkPhoneNumber,  ContactType.WorkPhoneNumber.ToString()},
                {(int)ContactType.PersonalEmail,  ContactType.PersonalEmail.ToString()},
                {(int)ContactType.WorkEmail,  ContactType.WorkEmail.ToString()},
                {(int)ContactType.Fax,  ContactType.Fax.ToString()},
                {(int)ContactType.Facebook,  ContactType.Facebook.ToString()},
                {(int)ContactType.LinkedIn,  ContactType.LinkedIn.ToString()},
            };
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
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)HotelContactInfoEventType.Updated, contactInfo);

        }
    }
}
