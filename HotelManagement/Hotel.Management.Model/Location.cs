
namespace Hotel.Management.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string Zip { get; set; }
        public int CityId { get; set; }
        public string? GeoLocation { get; set; }
    }
}
