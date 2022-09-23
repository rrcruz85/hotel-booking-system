using Reservation.Management.DataAccess.Extensions;
using Reservation.Management.Service.Extensions;

namespace Reservation.Management.WebApi.Extensions
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
