
namespace UserAccount.Management.Service.Interfaces
{
    public interface IUserContactInfoService
    {
        Task<int> CreateUserContactInfoAsync(Model.UserContactInfo userContactInfo);
        Task UpdateUserContactInfoAsync(Model.UserContactInfo userContactInfo);
        Task DeleteUserContactInfoAsync(int id);
        Task<Model.UserContactInfo?> GetUserByIdAsync(int id);               
    }
}
