using Reservation.Management.DataAccess.Entities;

namespace Reservation.Management.Service.Translators
{
    public static class HotelContactInfoTranslator
    {
        public static HotelContactInfo ToEntity(this Model.HotelContactInfo contactInfo)
        {
            return new HotelContactInfo
            {          
                 Id = contactInfo.Id,
                 HotelId = contactInfo.HotelId,
                 Type = contactInfo.Type,
                 Value = contactInfo.Value
            };
        }         

        public static Model.HotelContactInfo ToModel(this HotelContactInfo contactInfo)
        {
            return new Model.HotelContactInfo
            {
               Id = contactInfo.Id,
               HotelId= contactInfo.HotelId,
               Type= contactInfo.Type,
               Value = contactInfo.Value
            };
        }
    }
}
