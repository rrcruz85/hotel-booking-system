using UserAccount.Management.DataAccess;
using UserAccount.Management.DataAccess.Extensions;
using UserAccount.Management.Event.Processor;
using UserAccount.Management.Event.Processor.Interfaces;
using UserAccount.Management.Event.Processor.Services;
using UserAccount.Management.Service.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace UserAccount.Management.Event.Processor
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<UserAccountManagementContext>();
            builder.Services.AddRepositories();
            builder.Services.AddBusinessServices();
            builder.Services.AddScoped<ICityEventProcessor, CityEventProcessor>();
        }
    }
}
