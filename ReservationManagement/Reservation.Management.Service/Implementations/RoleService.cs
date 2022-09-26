using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;

namespace Reservation.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly string TopicName = "RoleTopicName";

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;            
        }         

        public async Task<int> CreateRoleAsync(Role role)
        { 
            return await _roleRepository.AddAsync(role.ToEntity());            
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var Role = await _roleRepository.FirstOrDefaultAsync(c => c.Id == roleId);
            if (Role == null)
            {
                throw new ArgumentException($"Role {roleId} does not exist");
            }
            await _roleRepository.DeleteAsync(Role);
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
        }
    }
}
