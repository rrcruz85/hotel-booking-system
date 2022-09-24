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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfiguration _config;
        private readonly string TopicName = "UserTopicName";
        public UserService(
            IUserRepository userRepository,
            IMessagingEngine messagingEngine,
            IConfiguration config)
        {
            _userRepository = userRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }         

        public async Task<int> CreateUserAsync(User user)
        { 
            var id = await _userRepository.AddAsync(user.ToEntity());
            user.Id = id;
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)UserEventType.Created, user);
            return id;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userRepository.FirstOrDefaultAsync(c => c.Id == userId);
            if (user == null)
            {
                throw new ArgumentException($"User {userId} does not exist");
            }
            await _userRepository.DeleteAsync(user);
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)UserEventType.Deleted, userId);
        }


        public async Task<User?> GetUserByIdAsync(int userId)
        {
            var entity = await _userRepository.FirstOrDefaultAsync(c => c.Id == userId);
            return entity?.ToModel();
        }

       
        public async Task UpdateUserAsync(User user)
        {
            var entity = await _userRepository.FirstOrDefaultAsync(c => c.Id == user.Id);
            if (entity == null)
            {
                throw new ArgumentException($"User {user.Id} does not exist");
            }             
            await _userRepository.UpdateAsync(user.ToEntity());
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)UserEventType.Updated, user);
        }
    }
}
