using Reservation.Management.DataAccess.Extensions;
using Reservation.Management.Service.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Reservation.Management.WebApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollection
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddRepositories();
            service.AddBusinessServices();
        }
    }

}
