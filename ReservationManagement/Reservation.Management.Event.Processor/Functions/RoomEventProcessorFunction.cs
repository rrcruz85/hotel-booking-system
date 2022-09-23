using System;
using System.Text.Json;
using System.Threading.Tasks;
using Hotel.Booking.Common.Contract.Messaging;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Reservation.Management.Event.Processor.Interfaces;

namespace Reservation.Management.Event.Processor
{
    public class RoomEventProcessorFunction
    {
        private readonly ILogger<RoomEventProcessorFunction> _logger;
        private readonly IRoomEventProcessor _roomEventProcessor;        

        public RoomEventProcessorFunction(
            ILogger<RoomEventProcessorFunction> log, 
            IRoomEventProcessor roomEventProcessor
            )
        {
            _logger = log;
            _roomEventProcessor = roomEventProcessor;             
        }

        [FunctionName("RoomEventProcessor")]
        public async Task RoomEventProcessorAsync([ServiceBusTrigger("room-events", "room-event-processor", Connection = "EventMessageConnectionString")] string @eventMessage)
        {
            try
            {
                _logger.LogInformation($"{nameof(RoomEventProcessorAsync)} function received message: {@eventMessage}");

                var @event = JsonSerializer.Deserialize<EventMessage<string>>(@eventMessage);

                await _roomEventProcessor.ProcessRoomEventAsync(@event.EventType, @event.Payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(RoomEventProcessorAsync)} failed to process message: {@eventMessage}, Reason: {ex.Message}");
            }
        }         
    }
}
