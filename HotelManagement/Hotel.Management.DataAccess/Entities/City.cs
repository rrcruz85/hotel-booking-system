using Hotel.Booking.Contract;

namespace Hotel.Management.DataAccess.Entities
{
    public partial class City: IEntity
    {
        public City()
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;

        public virtual ICollection<Location> Locations { get; set; }
    }
}
