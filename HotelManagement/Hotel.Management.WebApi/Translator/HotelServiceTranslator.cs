using Hotel.Management.WebApi.Models.Requests;
using Hotel.Management.Model;

namespace Hotel.Management.WebApi.Translator
{
    public static class HotelServiceTranslator
    {
        public static HotelService ToBusinessModel(this CreateHotelService model)
        {
            return new HotelService
            {
                HotelId = model.HotelId,
                Name = model.Name,
                Description = model.Description,
            };
        }

        public static HotelService ToBusinessModel(this UpdateHotelService model)
        {
            return new HotelService
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                HotelId = model.HotelId
            };
        }
    }
}
