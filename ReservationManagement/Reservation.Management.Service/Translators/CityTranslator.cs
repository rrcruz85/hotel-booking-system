
namespace Reservation.Management.Service.Translators
{
    public static class CityTranslator
    {
        public static DataAccess.Entities.City ToEntity(this Model.City city)
        {
            return new DataAccess.Entities.City
            {
                Id = city.Id,
                Name = city.Name,
                Country = city.Country,
                State = city.State
            };
        }

        public static Model.City ToModel(this DataAccess.Entities.City city)
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
