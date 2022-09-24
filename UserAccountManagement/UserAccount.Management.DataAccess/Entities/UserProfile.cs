using Hotel.Booking.Common.Contract.DataAccess;
using System;
using System.Collections.Generic;

namespace UserAccount.Management.DataAccess.Entities
{
    public partial class UserProfile: IEntity
    {
        public UserProfile()
        {
            UserContactInfos = new HashSet<UserContactInfo>();
        }

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

        public virtual City City { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<UserContactInfo> UserContactInfos { get; set; }
    }
}
