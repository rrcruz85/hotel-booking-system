using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;

namespace Reservation.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
       
        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;            
        }         

        public async Task<int> CreateUserProfileAsync(UserProfile user)
        { 
            return await _userProfileRepository.AddAsync(user.ToEntity());
        }

        public async Task DeleteUserProfileAsync(int userId)
        {
            var user = await _userProfileRepository.FirstOrDefaultAsync(c => c.Id == userId);
            if (user == null)
            {
                throw new ArgumentException($"User {userId} does not exist");
            }
            await _userProfileRepository.DeleteAsync(user);
        } 
       
        public async Task<UserProfile?> GetUserByIdAsync(int userId)
        {
            var entity = await _userProfileRepository.FirstOrDefaultAsync(c => c.Id == userId);
            return entity?.ToModel();
        }

       
        public async Task UpdateUserProfileAsync(UserProfile user)
        {
            var entity = await _userProfileRepository.FirstOrDefaultAsync(c => c.Id == user.Id);
            if (entity == null)
            {
                throw new ArgumentException($"User {user.Id} does not exist");
            }             
            await _userProfileRepository.UpdateAsync(user.ToEntity());
        }
    }
}
