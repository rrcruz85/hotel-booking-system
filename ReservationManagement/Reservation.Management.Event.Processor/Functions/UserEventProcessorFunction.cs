using System;
using System.Text.Json;
using System.Threading.Tasks;
using Hotel.Booking.Common.Contract.Messaging;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Reservation.Management.Event.Processor.Interfaces;

namespace Reservation.Management.Event.Processor
{
    public class UserEventProcessorFunction
    {
        private readonly ILogger<UserEventProcessorFunction> _logger;
         
        private readonly IUserEventProcessor _userEventProcessor;

        public UserEventProcessorFunction(
            ILogger<UserEventProcessorFunction> log,
            IUserEventProcessor userEventProcessor)
        {
            _logger = log;
            _userEventProcessor = userEventProcessor;
        }
 
        [FunctionName("UserEventProcessor")]
        public async Task UserEventProcessorAsync([ServiceBusTrigger("user-events", "user-event-processor", Connection = "EventMessageConnectionString")] string @eventMessage)
        {
            try
            {
                _logger.LogInformation($"{nameof(UserEventProcessorAsync)} function received message: {@eventMessage}");

                var @event = JsonSerializer.Deserialize<EventMessage<string>>(@eventMessage);

                await _userEventProcessor.ProcessUserEventAsync(@event.EventType, @event.Payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(UserEventProcessorAsync)} failed to process message: {@eventMessage}, Reason: {ex.Message}");
            }
        }
    }
}
