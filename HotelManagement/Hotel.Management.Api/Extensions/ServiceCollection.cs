using Hotel.Management.DataAccess.Extensions;
using Hotel.Management.Service.Extensions;

namespace Hotel.Management.Api.Extensions
{
    public static class ServiceCollection
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddRepositories();
            service.AddBusinessServices();
        }
    }

}
