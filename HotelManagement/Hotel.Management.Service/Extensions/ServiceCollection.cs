using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Booking.Common.Contract.Services;
using Hotel.Booking.Common.Messaging;
using Hotel.Booking.Common.Service;
using Hotel.Management.Service.Implementations;
using Hotel.Management.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Hotel.Management.Service.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollection
    {
        public static void AddBusinessServices(this IServiceCollection service)
        {
            service.AddScoped<ICityService, CityService>();
            service.AddScoped<IHotelCategoryRelationService, HotelCategoryRelationService>();
            service.AddScoped<IHotelCategoryService, HotelCategoryService>();
            service.AddScoped<IHotelContactInfoService, HotelContactInfoService>();
            service.AddScoped<IHotelFacilityService, HotelFacilityService>();
            service.AddScoped<IHotelGalleryService, HotelGalleryService>();
            service.AddScoped<IHotelService, HotelService>();
            service.AddScoped<IServicesForHotelService, ServicesForHotelService>();
            service.AddScoped<IRoomService, RoomService>();
            // Messaging
            service.AddScoped<IMessagingBroker, ServiceBusMessagingBroker>();
            service.AddScoped<IMessagingEngine, MessagingEngine>();
            // Blob 
            service.AddScoped<IBlobStorageService, BlobStorageService>();
            service.AddScoped<IConfigurationView, ConfigurationView>();
        }
    }
}
