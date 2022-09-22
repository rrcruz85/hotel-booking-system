using Reservation.Management.DataAccess.Entities;

namespace Reservation.Management.Service.Translators
{
    public static class HotelServiceTranslator
    {
        public static HotelService ToEntity(this Model.HotelService service)
        {
            return new HotelService
            {       
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                HotelId = service.HotelId
            };
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
