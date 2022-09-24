using Hotel.Booking.Common.Constant;
using Reservation.Management.Event.Processor.Interfaces;
using Reservation.Management.Service.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Services
{
    public class HotelCategoryEventProcessor : IHotelCategoryEventProcessor
    {
        private readonly IHotelCategoryService _hotelCategoryService;

        public HotelCategoryEventProcessor(IHotelCategoryService hotelCategoryService)
        {
            _hotelCategoryService = hotelCategoryService;
        }

        public async Task ProcessHotelCategoryEventAsync(int eventType, string eventPayload)
        {
            switch (eventType)
            {
                case (int)HotelCategoryEventType.Created:
                    {
                        var @event = JsonSerializer.Deserialize<Model.HotelCategory>(eventPayload);
                        await _hotelCategoryService.CreateHotelCategoryAsync(@event);
                    }
                    break;
                case (int)HotelCategoryEventType.Updated:
                    {
                        var @event = JsonSerializer.Deserialize<Model.HotelCategory>(eventPayload);
                        await _hotelCategoryService.UpdateHotelCategoryAsync(@event);
                    }
                    break;
                case (int)HotelCategoryEventType.Deleted:
                    {
                        var @event = JsonSerializer.Deserialize<int>(eventPayload);
                        await _hotelCategoryService.DeleteHotelCategoryAsync(@event);
                    }
                    break;
            }
        }
    }
}
