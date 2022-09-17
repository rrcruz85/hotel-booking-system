using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Booking.Common.Messaging;
using Hotel.Management.Service.Implementations;
using Hotel.Management.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Management.Service.Extensions
{
    public static class ServiceCollection
    {
        public static void AddBusinessServices(this IServiceCollection service)
        {
            service.AddScoped<ICityService, CityService>();
            service.AddScoped<IRoomService, RoomService>();
            // Messaging
            service.AddScoped<IMessagingBroker, ServiceBusMessagingBroker>();
            service.AddScoped<IMessagingEngine, MessagingEngine>();
        }
    }
}
