﻿using UserAccount.Management.DataAccess.Interfaces;
using UserAccount.Management.Model;
using UserAccount.Management.Service.Interfaces;
using UserAccount.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;
using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Booking.Common.Constant;
using Hotel.Booking.Common.Contract.Services;

namespace UserAccount.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfigurationView _config;
        private readonly string TopicName = "RolePermissionTopicName";

        public RolePermissionService(
            IRolePermissionRepository rolePermissionRepository,
            IMessagingEngine messagingEngine,
            IConfigurationView config)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }         

        public async Task<int> CreateRolePermissionAsync(RolePermission role)
        { 
            var  id = await _rolePermissionRepository.AddAsync(role.ToEntity());
            role.Id = id;
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)RolePermissionEventType.Created, role);
            return id;
        }

        public async Task DeleteRolePermissionAsync(int roleId)
        {
            var Role = await _rolePermissionRepository.FirstOrDefaultAsync(c => c.Id == roleId);
            if (Role == null)
            {
                throw new ArgumentException($"RolePermission {roleId} does not exist");
            }
            await _rolePermissionRepository.DeleteAsync(Role);
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)RolePermissionEventType.Deleted, roleId);
        }

        public async Task<RolePermission?> GetRolePermissionByIdAsync(int roleId)
        {
            var entity = await _rolePermissionRepository.FirstOrDefaultAsync(c => c.Id == roleId);
            return entity?.ToModel();
        }

        public async Task<List<RolePermission>> GetPermissionsByRoleIdAsync(int roleId)
        {
            var entities = await _rolePermissionRepository.WhereAsync(c => c.RoleId == roleId);
            return entities?.Select(r => r.ToModel()).ToList();
        }

        public async Task UpdateRolePermissionAsync(RolePermission role)
        {
            var entity = await _rolePermissionRepository.FirstOrDefaultAsync(c => c.Id == role.Id);
            if (entity == null)
            {
                throw new ArgumentException($"Role Permission {role.Id} does not exist");
            }             
            await _rolePermissionRepository.UpdateAsync(role.ToEntity());
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)RolePermissionEventType.Updated, role);

        }
    }
}
