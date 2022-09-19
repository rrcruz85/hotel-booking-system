namespace Hotel.Management.Model
{
    public class HotelContactInfo
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Value { get; set; } = null!;
        public int HotelId { get; set; }
    }
}
