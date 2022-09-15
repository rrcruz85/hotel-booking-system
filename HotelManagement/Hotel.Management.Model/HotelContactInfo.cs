namespace Hotel.Management.DataAccess.Entities
{
    public class HotelContactInfo
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public int HotelId { get; set; }
    }
}
