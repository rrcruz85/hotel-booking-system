using Hotel.Management.WebApi.Models.Requests;
using Hotel.Management.Model;

namespace Hotel.Management.WebApi.Translator
{
    public static class HotelCategoryTranslator
    {
        public static HotelCategory ToBusinessModel(this CreateHotelCategory model)
        {
            return new HotelCategory
            {
                Name = model.Name,
                Description = model.Description,
            };
        }

        public static HotelCategory ToBusinessModel(this UpdateHotelCategory model)
        {
            return new HotelCategory
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
            };
        }
    }
}
