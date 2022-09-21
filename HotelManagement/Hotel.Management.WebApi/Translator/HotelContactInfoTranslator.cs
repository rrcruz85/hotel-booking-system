using Hotel.Management.WebApi.Models.Requests;
using Hotel.Management.Model;

namespace Hotel.Management.WebApi.Translator
{
    public static class HotelContactInfoTranslator
    {
        public static HotelContactInfo ToBusinessModel(this CreateHotelContactInfo model)
        {
            return new HotelContactInfo
            {
                 HotelId = model.HotelId,
                 Type = model.Type, 
                 Value = model.Value,
            };
        }

        public static HotelContactInfo ToBusinessModel(this UpdateHotelContactInfo model)
        {
            return new HotelContactInfo
            {
                Id = model.Id,
                HotelId = model.HotelId,
                Type = model.Type,
                Value = model.Value,
            };
        }
    }
}
