
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
    }
}
