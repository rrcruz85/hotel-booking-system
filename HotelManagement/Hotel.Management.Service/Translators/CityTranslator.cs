using Hotel.Management.DataAccess.Entities;

namespace Hotel.Management.Service.Translators
{
    public static class CityTranslator
    {
        public static City ToNewEntity(this Model.City city)
        {
            return new City
            {                
                Name = city.Name,
                Country = city.Country,
                State = city.State
            };
        }

        public static City ToEntity(this Model.City city)
        {
            var entity = city.ToNewEntity();
            entity.Id = city.Id;
            return entity;
        }

        public static Model.City ToModel(this City city)
        {
            return new Model.City
            {
                Id = city.Id,
                Name = city.Name,
                Country = city.Country,
                State = city.State
            };
        }
    }
}
