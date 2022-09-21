using Hotel.Management.DataAccess.Entities;

namespace Hotel.Management.Service.Translators
{
    public static class HotelCategoryTranslator
    {
        public static HotelCategory ToNewEntity(this Model.HotelCategory category)
        {
            return new HotelCategory
            {                
                Name = category.Name,
                Description = category.Description
            };
        }

        public static HotelCategory ToEntity(this Model.HotelCategory category)
        {
            var entity = category.ToNewEntity();
            entity.Id = category.Id;
            return entity;
        }

        public static Model.HotelCategory ToModel(this HotelCategory category)
        {
            return new Model.HotelCategory
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public static Model.HotelCategoryRelation ToModel(this HotelCategoryRelation relation)
        {
            return new Model.HotelCategoryRelation
            {
                Id = relation.Id,
                HotelCategoryId = relation.HotelCategoryId,
                HotelId = relation.HotelId
            };
        }
    }
}
