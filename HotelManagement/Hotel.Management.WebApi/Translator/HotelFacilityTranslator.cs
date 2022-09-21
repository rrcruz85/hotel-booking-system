using Hotel.Management.WebApi.Models.Requests;
using Hotel.Management.Model;

namespace Hotel.Management.WebApi.Translator
{
    public static class HotelFacilityTranslator
    {
        public static HotelFacility ToBusinessModel(this CreateHotelFacility model)
        {
            return new HotelFacility
            {
                HotelId = model.HotelId,
                Name = model.Name,
                Description = model.Description,
            };
        }

        public static HotelFacility ToBusinessModel(this UpdateHotelFacility model)
        {
            return new HotelFacility
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                HotelId = model.HotelId
            };
        }
    }
}
