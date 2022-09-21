using Hotel.Booking.Common.Contract.DataAccess;

namespace Hotel.Management.DataAccess.Entities
{
    public partial class HotelCategory: IEntity
    {
        public HotelCategory()
        {
            HotelCategoryRelations = new HashSet<HotelCategoryRelation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<HotelCategoryRelation> HotelCategoryRelations { get; set; }
    }
}
