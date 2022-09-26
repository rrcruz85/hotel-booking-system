using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;

namespace Reservation.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionService(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }         

        public async Task<int> CreateRolePermissionAsync(RolePermission role)
        { 
            return await _rolePermissionRepository.AddAsync(role.ToEntity());
        }

        public async Task DeleteRolePermissionAsync(int roleId)
        {
            var Role = await _rolePermissionRepository.FirstOrDefaultAsync(c => c.Id == roleId);
            if (Role == null)
            {
                throw new ArgumentException($"RolePermission {roleId} does not exist");
            }
            await _rolePermissionRepository.DeleteAsync(Role);
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
        }
    }
}
