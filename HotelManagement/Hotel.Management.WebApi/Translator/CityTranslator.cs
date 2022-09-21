using Hotel.Management.WebApi.Models.Requests;

namespace Hotel.Management.WebApi.Translator
{
    public static class CityTranslator
    {
        public static Model.City ToBusinessModel(this CreateCity model)
        {
            return new Model.City
            {
                Name = model.Name,
                Country = model.Country,
                State = model.State
            };
        }

        public static Model.City ToBusinessModel(this UpdateCity model)
        {
            return new Model.City
            {
                Id = model.Id,
                Name = model.Name,
                State = model.State,
                Country = model.Country,
            };
        }
    }
}
