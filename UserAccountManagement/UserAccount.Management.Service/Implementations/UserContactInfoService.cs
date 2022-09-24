using UserAccount.Management.DataAccess.Interfaces;
using UserAccount.Management.Model;
using UserAccount.Management.Service.Interfaces;
using UserAccount.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;
using Hotel.Booking.Common.Contract.Messaging;
using Microsoft.Extensions.Configuration;
using Hotel.Booking.Common.Constant;

namespace UserAccount.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class UserContactInfoService : IUserContactInfoService
    {
        private readonly IUserContactInfoRepository _userContactInfoRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfiguration _config;
        private readonly string TopicName = "UserContactInfoTopicName";

        public UserContactInfoService(
            IUserContactInfoRepository userContactInfoRepository,
            IMessagingEngine messagingEngine,
            IConfiguration config)
        {
            _userContactInfoRepository = userContactInfoRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }         

        public async Task<int> CreateUserContactInfoAsync(UserContactInfo user)
        { 
            var id = await _userContactInfoRepository.AddAsync(user.ToEntity());
            user.Id = id;
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)UserContactInfoEventType.Created, user);
            return id;
        }

        public async Task DeleteUserContactInfoAsync(int userId)
        {
            var user = await _userContactInfoRepository.FirstOrDefaultAsync(c => c.Id == userId);
            if (user == null)
            {
                throw new ArgumentException($"User contact info {userId} does not exist");
            }
            await _userContactInfoRepository.DeleteAsync(user);
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)UserContactInfoEventType.Deleted, userId);
        }

        public async Task<UserContactInfo?> GetUserByIdAsync(int userId)
        {
            var entity = await _userContactInfoRepository.FirstOrDefaultAsync(c => c.Id == userId);
            return entity?.ToModel();
        }

       
        public async Task UpdateUserContactInfoAsync(UserContactInfo user)
        {
            var entity = await _userContactInfoRepository.FirstOrDefaultAsync(c => c.Id == user.Id);
            if (entity == null)
            {
                throw new ArgumentException($"User contact info {user.Id} does not exist");
            }             
            await _userContactInfoRepository.UpdateAsync(user.ToEntity());
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)UserContactInfoEventType.Updated, user);
        }
    }
}
