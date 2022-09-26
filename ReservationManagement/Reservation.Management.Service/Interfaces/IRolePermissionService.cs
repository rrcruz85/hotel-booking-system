
namespace Reservation.Management.Service.Interfaces
{
    public interface IRolePermissionService
    {
        Task<int> CreateRolePermissionAsync(Model.RolePermission role);
        Task UpdateRolePermissionAsync(Model.RolePermission role);
        Task DeleteRolePermissionAsync(int Id);
        Task<Model.RolePermission?> GetRolePermissionByIdAsync(int id);
        Task<List<Model.RolePermission>> GetPermissionsByRoleIdAsync(int roleId);
    }
}
