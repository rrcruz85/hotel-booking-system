using System.Diagnostics.CodeAnalysis;

namespace UserAccount.Management.WebApi.Translator
{
    [ExcludeFromCodeCoverage]
    public static class RoleTranslator
    {
        public static Model.Role ToNewModel(this Models.Requests.Role role)
        {
            return new Model.Role
            {
                Name = role.Name
            };
        }

        public static Model.Role ToModel(this Models.Requests.Role role)
        {
            return new Model.Role
            { 
                Id = role.Id,
                Name = role.Name                
            };
        }
    }
}
