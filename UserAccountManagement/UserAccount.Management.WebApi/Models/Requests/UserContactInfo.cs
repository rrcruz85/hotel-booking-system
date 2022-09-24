
namespace UserAccount.Management.WebApi.Models.Requests
{
    public class UserContactInfo
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Value { get; set; } = null!;
        public int ProfileId { get; set; }
    }
}
