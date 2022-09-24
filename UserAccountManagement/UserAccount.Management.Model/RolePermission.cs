
namespace UserAccount.Management.Model
{
    public class RolePermission
    {
        public int Id { get; set; }
        public int Action { get; set; }
        public bool Allowed { get; set; }
        public int RoleId { get; set; }
    }
}
