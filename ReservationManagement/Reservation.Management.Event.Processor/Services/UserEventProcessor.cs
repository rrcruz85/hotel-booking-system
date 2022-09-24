using Hotel.Booking.Common.Constant;
using Reservation.Management.Event.Processor.Interfaces;
using Reservation.Management.Service.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Services
{
    public class UserProfileEventProcessor : IUserProfileEventProcessor
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileEventProcessor(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task ProcessUserProfileEventAsync(int eventType, string eventPayload)
        {
            switch (eventType)
            {
                case (int)UserEventType.Created:
                    {
                        var @event = JsonSerializer.Deserialize<Model.UserProfile>(eventPayload);
                        await _userProfileService.CreateUserProfileAsync(@event);
                    }
                    break;
                case (int)UserEventType.Updated:
                    {
                        var @event = JsonSerializer.Deserialize<Model.UserProfile>(eventPayload);
                        await _userProfileService.UpdateUserProfileAsync(@event);
                    }
                    break;
                case (int)UserEventType.Deleted:
                    {
                        var @event = JsonSerializer.Deserialize<int>(eventPayload);
                        await _userProfileService.DeleteUserProfileAsync(@event);
                    }
                    break;
            }
        }
    }
}
