
using Hotel.Management.DataAccess.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Hotel.Management.Service.Translators
{
    [ExcludeFromCodeCoverage]
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
