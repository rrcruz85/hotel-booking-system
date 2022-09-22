namespace Reservation.Management.Model
{ 
    public class HotelCategoryRelation
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int HotelCategoryId { get; set; }
    }
}
