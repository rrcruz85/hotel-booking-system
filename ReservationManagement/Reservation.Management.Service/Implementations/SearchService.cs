using Hotel.Booking.Common.Constant;
using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Translators;
using Reservation.Management.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Reservation.Management.Service.Implementations
{
    public class SearchService : ISearchService
    {
        private readonly IHotelRepository _hotelRepository;
        public SearchService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<List<Room>> Search(SearchCriteria searchCriteria)
        {
            if (searchCriteria.StartTime >= searchCriteria.EndTime)
            {
                throw new ArgumentException("Start date can not be higher than End date");
            }

            if (searchCriteria.StartTime <= DateTime.Now.AddDays(1).Date)
            {
                throw new ArgumentException("Start date can not be prior to current date");
            }

            if (string.IsNullOrEmpty(searchCriteria.City) && !searchCriteria.CityId.HasValue && string.IsNullOrEmpty(searchCriteria.Country))
            {
                throw new ArgumentException("You have to provide at least the city and country where to look up at it");
            }

            var rooms = await _hotelRepository.WhereQueryable(x => !searchCriteria.CityId.HasValue || x.CityId == searchCriteria.CityId)
                .Where(x => string.IsNullOrEmpty(searchCriteria.City) || x.City.Name.Contains(searchCriteria.City))
                .Where(x => string.IsNullOrEmpty(searchCriteria.State) || x.City.State.Contains(searchCriteria.State))
                .Where(x => string.IsNullOrEmpty(searchCriteria.Country) || x.City.Country.Contains(searchCriteria.Country))
                .Where(x => string.IsNullOrEmpty(searchCriteria.Hotel) || x.Name.Contains(searchCriteria.Hotel))
                .Where(x => !searchCriteria.HotelId.HasValue || x.Id == searchCriteria.HotelId)
                .Where(x => !searchCriteria.HotelCategoryId.HasValue || x.HotelCategoryRelations.Any(c => c.HotelCategoryId == searchCriteria.HotelCategoryId))
                .SelectMany(x => x.Rooms
                    .Where(r => !searchCriteria.MinPrice.HasValue || r.CurrentPrice >= searchCriteria.MinPrice)
                    .Where(r => !searchCriteria.MaxPrice.HasValue || r.CurrentPrice <= searchCriteria.MaxPrice)
                    .Where(r => !searchCriteria.RoomType.HasValue || r.Type == searchCriteria.RoomType)
                    .Where(r => !r.RoomReservations.Any(rr => rr.Reservation.Status != (int)ReservationStatus.Canceled
                        && rr.Reservation.StartDate.Date <= searchCriteria.EndTime.Date
                        && rr.Reservation.EndDate.Date >= searchCriteria.StartTime.Date))
                    .OrderBy(x => x.Number))
                .ToListAsync();

            return rooms.Select(r => r.ToModel()).ToList();
        }
    }
}
