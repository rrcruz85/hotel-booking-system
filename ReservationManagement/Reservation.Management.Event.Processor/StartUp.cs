using Reservation.Management.DataAccess;
using Reservation.Management.DataAccess.Extensions;
using Reservation.Management.Event.Processor;
using Reservation.Management.Event.Processor.Interfaces;
using Reservation.Management.Event.Processor.Services;
using Reservation.Management.Service.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Reservation.Management.Event.Processor
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<ReservationManagementContext>();
            builder.Services.AddRepositories();
            builder.Services.AddBusinessServices();
            builder.Services.AddScoped<IRoomEventProcessor, RoomEventProcessor>();
            builder.Services.AddScoped<ICityEventProcessor, CityEventProcessor>();
            builder.Services.AddScoped<IHotelEventProcessor, HotelEventProcessor>();
            builder.Services.AddScoped<IHotelCategoryEventProcessor, HotelCategoryEventProcessor>();
            builder.Services.AddScoped<IUserEventProcessor, UserEventProcessor>();
            builder.Services.AddScoped<IUserProfileEventProcessor, UserProfileEventProcessor>();
            builder.Services.AddScoped<IRoleEventProcessor, RoleEventProcessor>();
        }
    }
}
