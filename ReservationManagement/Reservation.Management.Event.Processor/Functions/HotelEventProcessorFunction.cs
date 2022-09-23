using System;
using System.Text.Json;
using System.Threading.Tasks;
using Hotel.Booking.Common.Contract.Messaging;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Reservation.Management.Event.Processor.Interfaces;

namespace Reservation.Management.Event.Processor
{
    public class HotelEventProcessorFunction
    {
        private readonly ILogger<HotelEventProcessorFunction> _logger;
        private readonly IHotelEventProcessor _hotelEventProcessor;
       
        public HotelEventProcessorFunction(
            ILogger<HotelEventProcessorFunction> log, 
            IHotelEventProcessor hotelEventProcessor)
        {
            _logger = log;           
            _hotelEventProcessor = hotelEventProcessor;
        }

        
        [FunctionName("HotelEventProcessor")]
        public async Task HotelEventProcessorAsync([ServiceBusTrigger("hotel-events", "hotel-event-processor", Connection = "EventMessageConnectionString")] string @eventMessage)
        {
            try
            {
                _logger.LogInformation($"{nameof(HotelEventProcessorAsync)} function received message: {@eventMessage}");

                var @event = JsonSerializer.Deserialize<EventMessage<string>>(@eventMessage);

                await _hotelEventProcessor.ProcessHotelEventAsync(@event.EventType, @event.Payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(HotelEventProcessorAsync)} failed to process message: {@eventMessage}, Reason: {ex.Message}");
            }
        }
    }
}
