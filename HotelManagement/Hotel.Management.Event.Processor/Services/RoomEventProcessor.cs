using Hotel.Booking.Common.Constant;
using Hotel.Management.Event.Processor.Interfaces;
using Hotel.Management.Model;
using Hotel.Management.Service.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hotel.Management.Event.Processor.Services
{
    public class RoomEventProcessor : IRoomEventProcessor
    {
        private readonly IRoomService _roomService;

        public RoomEventProcessor(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task ProcessRoomEventAsync(int eventType, string eventPayload)
        {
            switch (eventType)
            {
                case (int)RoomEventType.Available:
                case (int)RoomEventType.Booked:
                case (int)RoomEventType.OutOfService:
                    {
                        var @event = JsonSerializer.Deserialize<RoomStatusEvent>(eventPayload);
                        await _roomService.UpdateRoomStatusAsync(@event.RoomId, @event.Status);
                    }
                    break;
            }
        }
    }
}
