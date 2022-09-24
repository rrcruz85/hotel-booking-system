using UserAccount.Management.DataAccess.Extensions;
using UserAccount.Management.Service.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace UserAccount.Management.WebApi.Extensions
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
