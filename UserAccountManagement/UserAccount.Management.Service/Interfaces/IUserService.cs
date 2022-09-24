
namespace UserAccount.Management.Service.Interfaces
{
    public interface IUserService
    {
        Task<int> CreateUserAsync(Model.User user);
        Task UpdateUserAsync(Model.User user);
        Task DeleteUserAsync(int userId);
        Task<Model.User?> GetUserByIdAsync(int userId);               
    }
}
