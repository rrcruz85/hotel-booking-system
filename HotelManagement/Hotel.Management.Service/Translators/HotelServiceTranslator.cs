using Hotel.Management.DataAccess.Entities;

namespace Hotel.Management.Service.Translators
{
    public static class HotelServiceTranslator
    {
        public static HotelService ToNewEntity(this Model.HotelService service)
        {
            return new HotelService
            {                
                Name = service.Name,
                Description = service.Description,
                HotelId = service.HotelId
            };
        }

        public static HotelService ToEntity(this Model.HotelService service)
        {
            var entity = service.ToNewEntity();
            entity.Id = service.Id;
            return entity;
        }

        public static Model.HotelService ToModel(this HotelService service)
        {
            return new Model.HotelService
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                HotelId = service.HotelId
            };
        }
    }
}
