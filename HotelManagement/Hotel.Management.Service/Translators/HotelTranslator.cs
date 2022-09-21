
namespace Hotel.Management.Service.Translators
{
    public static class HotelTranslator
    {
        public static DataAccess.Entities.Hotel ToNewEntity(this Model.Hotel hotel)
        {
            return new DataAccess.Entities.Hotel
            {                
                 AddressLine1 = hotel.AddressLine1,
                 AddressLine2 = hotel.AddressLine2,
                 CityId = hotel.CityId,
                 GeoLocation = hotel.GeoLocation, 
                 Name = hotel.Name,
                 Zip = hotel.Zip
            };
        }

        public static DataAccess.Entities.Hotel ToEntity(this Model.Hotel hotel)
        {
            var entity = hotel.ToNewEntity();
            entity.Id = hotel.Id;
            return entity;
        }

        public static Model.Hotel ToModel(this DataAccess.Entities.Hotel hotel)
        {
            return new Model.Hotel
            {
                Id = hotel.Id,
                AddressLine1 = hotel.AddressLine1,
                AddressLine2 = hotel.AddressLine2,
                CityId = hotel.CityId,
                GeoLocation = hotel.GeoLocation,
                Name = hotel.Name,
                Zip = hotel.Zip
            };
        }

        public static Model.HotelDetails ToDetailedModel(this DataAccess.Entities.Hotel hotel)
        {
            return new Model.HotelDetails
            {
                Id = hotel.Id,
                AddressLine1 = hotel.AddressLine1,
                AddressLine2 = hotel.AddressLine2,
                CityId = hotel.CityId,
                GeoLocation = hotel.GeoLocation,
                Name = hotel.Name,
                Zip = hotel.Zip,
                City = hotel.City.ToModel(),
                HotelCategoryRelations = hotel.HotelCategoryRelations.Select(x => x.ToModel()).ToList(),
                HotelContactInfos = hotel.HotelContactInfos.Select(x => x.ToModel()).ToList(),
                HotelFacilities = hotel.HotelFacilities.Select(x => x.ToModel()).ToList(),
                HotelGalleries = hotel.HotelGalleries.Select(x => x.ToGalleryModel()).ToList(),
                HotelServices = hotel.HotelServices.Select(x => x.ToModel()).ToList(),
                Rooms = hotel.Rooms.Select(x => x.ToModel()).ToList()                
            };
        }
    }
}
