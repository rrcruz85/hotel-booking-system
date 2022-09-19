using Hotel.Management.DataAccess.Interfaces;
using Hotel.Management.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Management.DataAccess.Extensions
{
    public static class ServiceCollection
    {
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<ICityRepository, CityRepository>();
            service.AddScoped<IHotelCategoryRepository, HotelCategoryRepository>();
            service.AddScoped<IHotelCategoryRelationRepository, HotelCategoryRelationRepository>();
            service.AddScoped<IHotelContactInfoRepository, HotelContactInfoRepository>();
            service.AddScoped<IHotelFacilityRepository, HotelFacilityRepository>();
            service.AddScoped<IHotelGalleryRepository, HotelGalleryRepository>();
            service.AddScoped<IHotelRepository, HotelRepository>();
            service.AddScoped<IHotelServiceRepository, HotelServiceRepository>();
            service.AddScoped<IRoomRepository, RoomRepository>();
        }
    }   
}
