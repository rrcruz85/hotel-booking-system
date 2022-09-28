using System;
using System.Text.Json;
using System.Threading.Tasks;
using Hotel.Booking.Common.Contract.Messaging;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Reservation.Management.Event.Processor.Interfaces;

namespace Reservation.Management.Event.Processor
{
    public class RoleEventProcessorFunction
    {
        private readonly ILogger<RoleEventProcessorFunction> _logger;
         
        private readonly IRoleEventProcessor _roleEventProcessor;

        public RoleEventProcessorFunction(
            ILogger<RoleEventProcessorFunction> log,
            IRoleEventProcessor roleEventProcessor)
        {
            _logger = log;
            _roleEventProcessor = roleEventProcessor;
        }
 
        [FunctionName("RoleEventProcessor")]
        public async Task RoleEventProcessorAsync([ServiceBusTrigger("role-events", "role-event-processor", Connection = "EventMessageConnectionString")] string @eventMessage)
        {
            try
            {
                _logger.LogInformation($"{nameof(RoleEventProcessorAsync)} function received message: {@eventMessage}");

                var @event = JsonSerializer.Deserialize<EventMessage<string>>(@eventMessage);

                await _roleEventProcessor.ProcessRoleEventAsync(@event.EventType, @event.Payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(RoleEventProcessorAsync)} failed to process message: {@eventMessage}, Reason: {ex.Message}");
            }
        }
    }
}
