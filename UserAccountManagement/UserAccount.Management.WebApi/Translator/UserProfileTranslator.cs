using System.Diagnostics.CodeAnalysis;

namespace UserAccount.Management.WebApi.Translator
{
    [ExcludeFromCodeCoverage]
    public static class UserProfileTranslator
    {
        public static Model.UserProfile ToNewModel(this Models.Requests.UserProfile user)
        {
            return new Model.UserProfile
            {
                 FirstName = user.FirstName,
                 HomePhone = user.HomePhone,
                 LastName = user.LastName,
                 AddressLine1 = user.AddressLine1,
                 AddressLine2 = user.AddressLine2,
                 CityId = user.CityId,  
                 Dob = user.Dob,
                 Email = user.Email,
                 Gender = user.Gender,
                 IdType = user.IdType,
                 IdValue = user.IdValue,
                 Mobile = user.Mobile,
                 UserId = user.UserId,
                 Zip = user.Zip,
            };
        }

        public static Model.UserProfile ToModel(this Models.Requests.UserProfile user)
        {
            return new Model.UserProfile
            {
                Id = user.Id,
                FirstName = user.FirstName,
                HomePhone = user.HomePhone,
                LastName = user.LastName,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                CityId = user.CityId,
                Dob = user.Dob,
                Email = user.Email,
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
