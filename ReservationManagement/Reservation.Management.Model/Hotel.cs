
namespace Reservation.Management.Model
{
    public class Hotel 
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string? Zip { get; set; }
        public int CityId { get; set; }
        public string? GeoLocation { get; set; }
    }
}
