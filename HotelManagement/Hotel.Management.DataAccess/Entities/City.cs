using Hotel.Booking.Common.Contract.DataAccess;

namespace Hotel.Management.DataAccess.Entities
{
    public partial class City: IEntity
    {
        public City()
        {
            Hotels = new HashSet<Hotel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
