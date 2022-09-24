using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Booking.Common.Contract.Services;
using Hotel.Booking.Common.Messaging;
using Hotel.Booking.Common.Service;
using UserAccount.Management.Service.Implementations;
using UserAccount.Management.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace UserAccount.Management.Service.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollection
    {
        public static void AddBusinessServices(this IServiceCollection service)
        {
            service.AddScoped<ICityService, CityService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IUserProfileService, UserProfileService>();
            service.AddScoped<IUserContactInfoService, UserContactInfoService>();
            service.AddScoped<IRoleService, RoleService>();
            service.AddScoped<IRolePermissionService, RolePermissionService>();

            // Messaging
            service.AddScoped<IMessagingBroker, ServiceBusMessagingBroker>();
            service.AddScoped<IMessagingEngine, MessagingEngine>();
            // Blob 
            service.AddScoped<IBlobStorageService, BlobStorageService>();
        }
    }
}
