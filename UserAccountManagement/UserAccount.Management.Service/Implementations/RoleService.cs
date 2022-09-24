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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfiguration _config;
        private readonly string TopicName = "RoleTopicName";

        public RoleService(
            IRoleRepository roleRepository,
            IMessagingEngine messagingEngine,
            IConfiguration config)
        {
            _roleRepository = roleRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }         

        public async Task<int> CreateRoleAsync(Role role)
        { 
            var id = await _roleRepository.AddAsync(role.ToEntity());
            role.Id = id;
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)RoleEventType.Created, role);
            return id;
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var Role = await _roleRepository.FirstOrDefaultAsync(c => c.Id == roleId);
            if (Role == null)
            {
                throw new ArgumentException($"Role {roleId} does not exist");
            }
            await _roleRepository.DeleteAsync(Role);
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)RoleEventType.Deleted, roleId);
        }
       
        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            var entity = await _roleRepository.FirstOrDefaultAsync(c => c.Id == roleId);
            return entity?.ToModel();
        }
       
        public async Task UpdateRoleAsync(Role role)
        {
            var entity = await _roleRepository.FirstOrDefaultAsync(c => c.Id == role.Id);
            if (entity == null)
            {
                throw new ArgumentException($"Role {role.Id} does not exist");
            }             
            await _roleRepository.UpdateAsync(role.ToEntity());
            await _messagingEngine.PublishEventMessageAsync(_config[TopicName], (int)RoleEventType.Updated, role);
        }
    }
}
