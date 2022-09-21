using Hotel.Management.DataAccess.Extensions;
using Hotel.Management.Event.Processor;
using Hotel.Management.Event.Processor.Interfaces;
using Hotel.Management.Event.Processor.Services;
using Hotel.Management.Service.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Hotel.Management.Event.Processor
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddRepositories();
            builder.Services.AddBusinessServices();
            builder.Services.AddSingleton<IRoomEventProcessor, RoomEventProcessor>();
        }
    }
}
