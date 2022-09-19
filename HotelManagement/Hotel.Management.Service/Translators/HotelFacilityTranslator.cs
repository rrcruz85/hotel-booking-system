using Hotel.Management.DataAccess.Entities;

namespace Hotel.Management.Service.Translators
{
    public static class HotelFacilityTranslator
    {
        public static HotelFacility ToNewEntity(this Model.HotelFacility facility)
        {
            return new HotelFacility
            {                
                Name = facility.Name,
                Description = facility.Description,
                HotelId = facility.HotelId
            };
        }

        public static HotelFacility ToEntity(this Model.HotelFacility facility)
        {
            var entity = facility.ToNewEntity();
            entity.Id = facility.Id;
            return entity;
        }

        public static Model.HotelFacility ToModel(this HotelFacility facility)
        {
            return new Model.HotelFacility
            {
                Id = facility.Id,
                Name = facility.Name,
                Description = facility.Description,
                HotelId = facility.HotelId
            };
        }
    }
}
