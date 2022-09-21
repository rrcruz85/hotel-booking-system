using Hotel.Management.DataAccess.Entities;

namespace Hotel.Management.Service.Translators
{
    public static class ImageTranslator
    {
        public static Model.HotelImage ToModel(this HotelGallery image)
        {
            return new Model.HotelImage
            {
               HotelId = image.HotelId,
               Id = image.Id,
               BlobImageUri = image.BlobImageUri,
               Description= image.Description
            };
        }

        public static Model.HotelGallery ToGalleryModel(this HotelGallery image)
        {
            return new Model.HotelGallery
            {
                HotelId = image.HotelId,
                Id = image.Id,
                BlobImageUri = image.BlobImageUri,
                Description = image.Description
            };
        }
    }
}
