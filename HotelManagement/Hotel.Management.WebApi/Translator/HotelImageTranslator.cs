
namespace Hotel.Management.WebApi.Translator
{
    public static class HotelImageTranslator
    {
        public static Model.CreateHotelImage ToBusinessModel(this Models.Requests.CreateHotelImage model)
        {
            return new Model.CreateHotelImage
            {
                HotelId = model.HotelId,
                BlobImageContent = model.BlobImageContent,
                Description = model.Description
            };
        }

        public static Model.UpdateHotelImage ToBusinessModel(this Models.Requests.UpdateHotelImage model)
        {
            return new Model.UpdateHotelImage
            {
                Id = model.Id,
                BlobImageContent = model.BlobImageContent,
                Description = model.Description,
                HotelId = model.HotelId
            };
        }
    }
}
