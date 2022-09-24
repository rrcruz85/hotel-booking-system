using System.Diagnostics.CodeAnalysis;

namespace UserAccount.Management.WebApi.Translator
{
    [ExcludeFromCodeCoverage]
    public static class UserContactInfoTranslator
    {
        public static Model.UserContactInfo ToNewModel(this Models.Requests.UserContactInfo user)
        {
            return new Model.UserContactInfo
            {
                 ProfileId = user.ProfileId,    
                 Type = user.Type,
                 Value = user.Value,
            };
        }

        public static Model.UserContactInfo ToModel(this Models.Requests.UserContactInfo user)
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
