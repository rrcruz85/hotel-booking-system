namespace Hotel.Management.Model 
{ 
    public class HotelService
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int HotelId { get; set; }
    }
}
