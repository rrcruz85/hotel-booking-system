using Hotel.Management.DataAccess.Interfaces;
using Hotel.Management.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Management.DataAccess.Extensions
{
    public static class ServiceCollection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IHotelCategoryRepository, HotelCategoryRepository>();
            services.AddScoped<IHotelCategoryRelationRepository, HotelCategoryRelationRepository>();
            services.AddScoped<IHotelContactInfoRepository, HotelContactInfoRepository>();
            services.AddScoped<IHotelFacilityRepository, HotelFacilityRepository>();
            services.AddScoped<IHotelGalleryRepository, HotelGalleryRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IHotelServiceRepository, HotelServiceRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
        }
    }   
}
