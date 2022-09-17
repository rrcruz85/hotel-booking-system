using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Management.Event.Processor.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hotel.Management.Event.Processor
{
    public class EventProcessorFunction
    {
        private readonly ILogger<EventProcessorFunction> _logger;
        private readonly IRoomEventProcessor _roomEventProcessor;
        private readonly IReservationEventProcessor _reservationEventProcessor;

        public EventProcessorFunction(
            ILogger<EventProcessorFunction> log, 
            IRoomEventProcessor roomEventProcessor,
            IReservationEventProcessor reservationEventProcessor)
        {
            _logger = log;
            _roomEventProcessor = roomEventProcessor;
            _reservationEventProcessor = reservationEventProcessor;
        }

        [FunctionName("RoomEventProcessor")]
        public async Task RoomEventProcessorAsync([ServiceBusTrigger("hotel-booking", "room-events", Connection = "ServiceBusHotelBookingConnectionString")] string @eventMessage)
        {
            try
            {
                _logger.LogInformation($"{nameof(RoomEventProcessorAsync)} function received message: {@eventMessage}");

                var @event = JsonSerializer.Deserialize<EventMessage<string>>(@eventMessage);

                await _roomEventProcessor.ProcessRoomEventAsync(@event.EventType, @event.Payload);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"{nameof(RoomEventProcessorAsync)} failed to process message: {@eventMessage}, Reason: {ex.Message}");
            }
        }

        [FunctionName("ReservationEventProcessor")]
        public async Task ReservationEventProcessorAsync([ServiceBusTrigger("hotel-booking", "reservation-events", Connection = "ServiceBusHotelBookingConnectionString")] string @eventMessage)
        {
            try
            {
                _logger.LogInformation($"{nameof(ReservationEventProcessorAsync)} function received message: {@eventMessage}");

                var @event = JsonSerializer.Deserialize<EventMessage<string>>(@eventMessage);

                await _reservationEventProcessor.ProcessReservationEventAsync(@event.EventType, @event.Payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(ReservationEventProcessorAsync)} failed to process message: {@eventMessage}, Reason: {ex.Message}");
            }
        }
    }
}
