
namespace Reservation.Management.Service.Interfaces
{
    public interface IUserProfileService
    {
        Task<int> CreateUserProfileAsync(Model.UserProfile userProfile);
        Task UpdateUserProfileAsync(Model.UserProfile userProfile);
        Task DeleteUserProfileAsync(int userProfileId);
        Task<Model.UserProfile?> GetUserByIdAsync(int userId);               
    }
}
