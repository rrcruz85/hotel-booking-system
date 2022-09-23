using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;

namespace Reservation.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
       
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;            
        }         

        public async Task<int> CreateUserAsync(User user)
        { 
            return await _userRepository.AddAsync(user.ToEntity());
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userRepository.FirstOrDefaultAsync(c => c.Id == userId);
            if (user == null)
            {
                throw new ArgumentException($"User {userId} does not exist");
            }
            await _userRepository.DeleteAsync(user);
        }       

       
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            var entity = await _userRepository.FirstOrDefaultAsync(c => c.Id == userId);
            return entity?.ToModel();
        }

       
        public async Task UpdateUserAsync(User user)
        {
            var entity = await _userRepository.FirstOrDefaultAsync(c => c.Id == user.Id);
            if (entity == null)
            {
                throw new ArgumentException($"User {user.Id} does not exist");
            }             
            await _userRepository.UpdateAsync(user.ToEntity());
        }
    }
}
