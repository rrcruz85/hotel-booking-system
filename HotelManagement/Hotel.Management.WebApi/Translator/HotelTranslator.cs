using Hotel.Management.WebApi.Models.Requests;

namespace Hotel.Management.WebApi.Translator
{
    public static class HotelTranslator
    {
        public static Model.Hotel ToBusinessModel(this CreateHotel model)
        {
            return new Model.Hotel
            {
                Name = model.Name,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                CityId = model.CityId,
                GeoLocation = model.GeoLocation,
                Zip = model.Zip
            };
        }

        public static Model.Hotel ToBusinessModel(this UpdateHotel model)
        {
            return new Model.Hotel
            {
                Id = model.Id,
                Name = model.Name,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                CityId = model.CityId,
                GeoLocation = model.GeoLocation,
                Zip = model.Zip
            };
        }
    }
}
