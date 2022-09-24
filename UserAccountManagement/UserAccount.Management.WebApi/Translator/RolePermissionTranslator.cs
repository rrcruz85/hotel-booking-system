using System.Diagnostics.CodeAnalysis;

namespace UserAccount.Management.WebApi.Translator
{
    [ExcludeFromCodeCoverage]
    public static class RolePermissionTranslator
    {
        public static Model.RolePermission ToNewModel(this Models.Requests.RolePermission role)
        {
            return new Model.RolePermission
            {
                Action = role.Action,
                Allowed = role.Allowed,
                RoleId = role.RoleId
            };
        }

        public static Model.RolePermission ToModel(this Models.Requests.RolePermission role)
        {
            return new Model.RolePermission
            { 
                Id = role.Id,
                Action = role.Action,
                Allowed = role.Allowed,
                RoleId = role.RoleId
            };
        }
    }
}
