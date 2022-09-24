
namespace UserAccount.Management.WebApi.Models.Requests
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Gender { get; set; } = null!;
        public int IdType { get; set; }
        public string IdValue { get; set; } = null!;
        public int UserId { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string? Zip { get; set; }
        public int CityId { get; set; }
        public string Email { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string? HomePhone { get; set; }
    }
}
