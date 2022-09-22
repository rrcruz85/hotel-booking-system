using Reservation.Management.DataAccess.Entities;

namespace Reservation.Management.Service.Translators
{
    public static class ImageTranslator
    {
        public static Model.HotelGallery ToModel(this HotelGallery image)
        {
            return new Model.HotelGallery
            {
               HotelId = image.HotelId,
               Id = image.Id,
               BlobImageUri = image.BlobImageUri,
               Description= image.Description
            };
        }

        public static HotelGallery ToEntity(this Model.HotelGallery image)
        {
            return new HotelGallery
            {
                HotelId = image.HotelId,
                Id = image.Id,
                BlobImageUri = image.BlobImageUri,
                Description = image.Description
            };
        }
    }
}
