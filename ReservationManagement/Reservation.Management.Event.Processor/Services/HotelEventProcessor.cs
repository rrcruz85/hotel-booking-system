using Hotel.Booking.Common.Constant;
using Reservation.Management.Event.Processor.Interfaces;
using Reservation.Management.Service.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Services
{
    public class HotelEventProcessor : IHotelEventProcessor
    {
        private readonly IHotelService _hotelService;

        public HotelEventProcessor(IHotelService roomService)
        {
            _hotelService = roomService;
        }

        public async Task ProcessHotelEventAsync(int eventType, string eventPayload)
        {
            switch (eventType)
            {
                case (int)HotelEventType.Created:
                    {
                        var @event = JsonSerializer.Deserialize<Model.Hotel>(eventPayload);
                        await _hotelService.CreateHotelAsync(@event);
                    }
                    break;
                case (int)RoomEventType.Updated:
                    {
                        var @event = JsonSerializer.Deserialize<Model.Hotel>(eventPayload);
                        await _hotelService.UpdateHotelAsync(@event);
                    }
                    break;
                case (int)RoomEventType.Deleted:
                    {
                        var @event = JsonSerializer.Deserialize<int>(eventPayload);
                        await _hotelService.DeleteHotelAsync(@event);
                    }
                    break;
            }
        }
    }
}
