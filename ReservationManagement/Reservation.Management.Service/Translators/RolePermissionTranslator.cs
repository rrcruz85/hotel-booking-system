using Reservation.Management.DataAccess.Entities;

namespace Reservation.Management.Service.Translators
{
    public static class RolePermissionTranslator
    {
        public static RolePermission ToEntity(this Model.RolePermission role)
        {
            return new RolePermission
            {
               Id = role.Id,
               Action = role.Action,
               Allowed = role.Allowed,
               RoleId = role.RoleId,
            };
        }         

        public static Model.RolePermission ToModel(this RolePermission role)
        {
            return new Model.RolePermission
            {
                Id = role.Id,
                Action = role.Action,
                Allowed = role.Allowed,
                RoleId = role.RoleId,
            };
        }
    }
}
