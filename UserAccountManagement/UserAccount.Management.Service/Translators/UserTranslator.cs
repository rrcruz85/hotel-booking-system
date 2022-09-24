using UserAccount.Management.DataAccess.Entities;

namespace UserAccount.Management.Service.Translators
{
    public static class UserTranslator
    {
        public static User ToEntity(this Model.User user)
        {
            return new User
            {
               Id = user.Id,
               IsActive = user.IsActive,
               Password = user.Password,
               RoleId = user.RoleId,
               UserName = user.UserName
            };
        }         

        public static Model.User ToModel(this User user)
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
