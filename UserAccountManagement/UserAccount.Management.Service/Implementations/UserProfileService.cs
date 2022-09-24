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
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfiguration _config;
        private readonly string TopicName = "UserProfileTopicName";

        public UserProfileService(
            IUserProfileRepository userProfileRepository,
            IMessagingEngine messagingEngine,
            IConfiguration config
            )
        {
            _userProfileRepository = userProfileRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }         

        public async Task<int> CreateUserProfileAsync(UserProfile user)
        { 
            var id = await _userProfileRepository.AddAsync(user.ToEntity());
            user.Id = id;
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)UserProfileEventType.Created, user);
            return id;
        }

        public async Task DeleteUserProfileAsync(int id)
        {
            var user = await _userProfileRepository.FirstOrDefaultAsync(c => c.Id == id);
            if (user == null)
            {
                throw new ArgumentException($"User profile {id} does not exist");
            }
            await _userProfileRepository.DeleteAsync(user);
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)UserProfileEventType.Deleted, id);

        }

        public async Task<UserProfile?> GetUserByIdAsync(int userId)
        {
            var entity = await _userProfileRepository.FirstOrDefaultAsync(c => c.Id == userId);
            return entity?.ToModel();
        }

       
        public async Task UpdateUserProfileAsync(UserProfile user)
        {
            var entity = await _userProfileRepository.FirstOrDefaultAsync(c => c.Id == user.Id);
            if (entity == null)
            {
                throw new ArgumentException($"User profile {user.Id} does not exist");
            }             
            await _userProfileRepository.UpdateAsync(user.ToEntity());
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)UserProfileEventType.Updated, user);
        }
    }
}
