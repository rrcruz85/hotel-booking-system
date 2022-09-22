using Reservation.Management.DataAccess.Entities;

namespace Reservation.Management.Service.Translators
{
    public static class HotelFacilityTranslator
    {
        public static HotelFacility ToEntity(this Model.HotelFacility facility)
        {
            return new HotelFacility
            {     
                Id = facility.Id,
                Name = facility.Name,
                Description = facility.Description,
                HotelId = facility.HotelId
            };
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
