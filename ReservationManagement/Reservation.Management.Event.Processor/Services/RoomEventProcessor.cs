using Hotel.Booking.Common.Constant;
using Reservation.Management.Event.Processor.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Services
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
                case (int)RoomEventType.Created:
                    {
                        var @event = JsonSerializer.Deserialize<Room>(eventPayload);
                        await _roomService.CreateRoomAsync(@event);
                    }
                    break;
                case (int)RoomEventType.Updated:
                    {
                        var @event = JsonSerializer.Deserialize<Room>(eventPayload);
                        await _roomService.UpdateRoomAsync(@event);
                    }
                    break;
                case (int)RoomEventType.Deleted:
                    {
                        var @event = JsonSerializer.Deserialize<int>(eventPayload);
                        await _roomService.DeleteRoomAsync(@event);
                    }
                    break;
            }
        }
    }
}
