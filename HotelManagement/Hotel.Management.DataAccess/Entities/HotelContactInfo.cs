using Hotel.Booking.Contract.DataAccess;

namespace Hotel.Management.DataAccess.Entities
{
    public partial class HotelContactInfo: IEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// possible values are: email (1), mobile (2), phone (3), website (4), social network (5)
        /// </summary>
        public int Type { get; set; }
        public string Value { get; set; } = null!;
        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
    }
}
