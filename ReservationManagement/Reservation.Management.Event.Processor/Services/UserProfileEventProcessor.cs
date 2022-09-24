using Hotel.Booking.Common.Constant;
using Reservation.Management.Event.Processor.Interfaces;
using Reservation.Management.Service.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Services
{
    public class UserEventProcessor : IUserEventProcessor
    {
        private readonly IUserService _userService;

        public UserEventProcessor(IUserService userService)
        {
            _userService = userService;
        }

        public async Task ProcessUserEventAsync(int eventType, string eventPayload)
        {
            switch (eventType)
            {
                case (int)UserProfileEventType.Created:
                    {
                        var @event = JsonSerializer.Deserialize<Model.User>(eventPayload);
                        await _userService.CreateUserAsync(@event);
                    }
                    break;
                case (int)UserProfileEventType.Updated:
                    {
                        var @event = JsonSerializer.Deserialize<Model.User>(eventPayload);
                        await _userService.CreateUserAsync(@event);
                    }
                    break;
                case (int)UserProfileEventType.Deleted:
                    {
                        var @event = JsonSerializer.Deserialize<int>(eventPayload);
                        await _userService.DeleteUserAsync(@event);
                    }
                    break;
            }
        }
    }
}
