using System;
using System.Text.Json;
using System.Threading.Tasks;
using Hotel.Booking.Common.Contract.Messaging;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Reservation.Management.Event.Processor.Interfaces;

namespace Reservation.Management.Event.Processor
{
    public class UserProfileEventProcessorFunction
    {
        private readonly ILogger<UserProfileEventProcessorFunction> _logger;
         
        private readonly IUserProfileEventProcessor _UserProfileEventProcessor;

        public UserProfileEventProcessorFunction(
            ILogger<UserProfileEventProcessorFunction> log,             
            IUserProfileEventProcessor UserProfileEventProcessor)
        {
            _logger = log;
            _UserProfileEventProcessor = UserProfileEventProcessor;
        }
 
        [FunctionName("UserProfileEventProcessor")]
        public async Task UserProfileEventProcessorAsync([ServiceBusTrigger("user-profile-events", "user-profile-event-processor", Connection = "EventMessageConnectionString")] string @eventMessage)
        {
            try
            {
                _logger.LogInformation($"{nameof(UserProfileEventProcessorAsync)} function received message: {@eventMessage}");

                var @event = JsonSerializer.Deserialize<EventMessage<string>>(@eventMessage);

                await _UserProfileEventProcessor.ProcessUserProfileEventAsync(@event.EventType, @event.Payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserProfileEventProcessorAsync)} failed to process message: {@eventMessage}, Reason: {ex.Message}");
            }
        }
    }
}
