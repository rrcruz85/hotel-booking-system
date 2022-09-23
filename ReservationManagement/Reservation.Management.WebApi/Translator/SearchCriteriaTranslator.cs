
using System.Diagnostics.CodeAnalysis;

namespace Reservation.Management.WebApi.Translator
{
    [ExcludeFromCodeCoverage]
    public static class SearchCriteriaTranslator
    {
        public static Model.SearchCriteria ToBusinessModel(this Models.Requests.SearchCriteria reservation)
        {
            return new Model.SearchCriteria
            {
                 City = reservation.City,
                 Country = reservation.Country,
                 CityId = reservation.CityId,
                 EndTime = reservation.EndTime, 
                 Hotel = reservation.Hotel,
                 HotelCategoryId = reservation.HotelCategoryId,
                 HotelId = reservation.HotelId,
                 MaxPrice = reservation.MaxPrice,
                 MinPrice = reservation.MinPrice,
                 RoomType = reservation.RoomType,
                 StartTime = reservation.StartTime,
                 State = reservation.State                  
            };
        }
    }
}
