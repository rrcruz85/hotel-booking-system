using UserAccount.Management.DataAccess.Entities;

namespace UserAccount.Management.Service.Translators
{
    public static class UserProfileTranslator
    {
        public static UserProfile ToEntity(this Model.UserProfile user)
        {
            return new UserProfile
            {
               Id = user.Id,
               AddressLine1 = user.AddressLine1,
               AddressLine2 = user.AddressLine2,
               CityId = user.CityId,
               Dob = user.Dob,
               Email = user.Email,
               FirstName = user.FirstName,
               LastName = user.LastName,    
               HomePhone = user.HomePhone,
               Gender = user.Gender,
               IdType = user.IdType,
               IdValue = user.IdValue,
               Mobile = user.Mobile,
               UserId = user.UserId,
               Zip = user.Zip,
            };
        }         

        public static Model.UserProfile ToModel(this UserProfile user)
        {
            return new Model.UserProfile
            {
                Id = user.Id,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                CityId = user.CityId,
                Dob = user.Dob,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                HomePhone = user.HomePhone,
                Gender = user.Gender,
                IdType = user.IdType,
                IdValue = user.IdValue,
                Mobile = user.Mobile,
                UserId = user.UserId,
                Zip = user.Zip,
            };
        }
    }
}
