using UserAccount.Management.DataAccess.Entities;

namespace UserAccount.Management.Service.Translators
{
    public static class RoleTranslator
    {
        public static Role ToEntity(this Model.Role role)
        {
            return new Role
            {
               Id = role.Id,
               Name = role.Name
            };
        }         

        public static Model.Role ToModel(this Role role)
        {
            return new Model.Role
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
