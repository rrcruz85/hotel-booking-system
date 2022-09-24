
namespace UserAccount.Management.Service.Interfaces
{
    public interface IRoleService
    {
        Task<int> CreateRoleAsync(Model.Role role);
        Task UpdateRoleAsync(Model.Role role);
        Task DeleteRoleAsync(int Id);
        Task<Model.Role?> GetRoleByIdAsync(int id);               
    }
}
