using System.Diagnostics.CodeAnalysis;

namespace UserAccount.Management.WebApi.Translator
{
    [ExcludeFromCodeCoverage]
    public static class UserTranslator
    {
        public static Model.User ToNewModel(this Models.Requests.User user)
        {
            return new Model.User
            {
                IsActive = user.IsActive,
                Password = user.Password,
                RoleId = user.RoleId,
                UserName = user.UserName
            };
        }

        public static Model.User ToModel(this Models.Requests.User user)
        {
            return new Model.User
            {
                Id = user.Id,
                IsActive = user.IsActive,
                Password = user.Password,
                RoleId = user.RoleId,
                UserName = user.UserName
            };
        }
    }
}
