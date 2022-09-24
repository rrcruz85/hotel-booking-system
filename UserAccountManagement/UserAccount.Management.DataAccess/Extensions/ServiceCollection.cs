using UserAccount.Management.DataAccess.Interfaces;
using UserAccount.Management.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace UserAccount.Management.DataAccess.Extensions
{
    public static class ServiceCollection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserContactInfoRepository, UserContactInfoRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }   
}
