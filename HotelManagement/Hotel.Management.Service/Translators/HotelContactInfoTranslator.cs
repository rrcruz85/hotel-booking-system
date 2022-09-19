using Hotel.Management.DataAccess.Entities;

namespace Hotel.Management.Service.Translators
{
    public static class HotelContactInfoTranslator
    {
        public static HotelContactInfo ToNewEntity(this Model.HotelContactInfo contactInfo)
        {
            return new HotelContactInfo
            {                
                 HotelId = contactInfo.HotelId,
                 Type = contactInfo.Type,
                 Value = contactInfo.Value
            };
        }

        public static HotelContactInfo ToEntity(this Model.HotelContactInfo contactInfo)
        {
            var entity = contactInfo.ToNewEntity();
            entity.Id = contactInfo.Id;
            return entity;
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
