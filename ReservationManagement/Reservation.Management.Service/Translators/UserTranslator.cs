using Reservation.Management.DataAccess.Entities;

namespace Reservation.Management.Service.Translators
{
    public static class UserTranslator
    {
        public static User ToEntity(this Model.User user)
        {
            return new User
            {
               Id = user.Id,
               IsActive = user.IsActive,
               Passoword = user.Passoword,
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
                Passoword = user.Passoword,
                RoleId = user.RoleId,
                UserName = user.UserName
            };
        }
    }
}
