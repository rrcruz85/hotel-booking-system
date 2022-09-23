using Reservation.Management.DataAccess.Entities;

namespace Reservation.Management.Service.Translators
{
    public static class UserContactInfoTranslator
    {
        public static UserContactInfo ToEntity(this Model.UserContactInfo user)
        {
            return new UserContactInfo
            {
               Id = user.Id,
               ProfileId = user.ProfileId,
               Type = user.Type,
               Value = user.Value,
            };
        }         

        public static Model.UserContactInfo ToModel(this UserContactInfo user)
        {
            return new Model.UserContactInfo
            {
                Id = user.Id,
                ProfileId = user.ProfileId,
                Type = user.Type,
                Value = user.Value,
            };
        }
    }
}
