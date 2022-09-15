
namespace Hotel.Management.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public int HotelId { get; set; }
        public int? Floor { get; set; }
        public string? Extension { get; set; }
        public int Type { get; set; }
        public int MaxCapacity { get; set; }
        public int Status { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
