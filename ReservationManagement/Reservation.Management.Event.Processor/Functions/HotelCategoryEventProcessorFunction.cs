using System;
using System.Text.Json;
using System.Threading.Tasks;
using Hotel.Booking.Common.Contract.Messaging;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Reservation.Management.Event.Processor.Interfaces;

namespace Reservation.Management.Event.Processor
{
    public class HotelCategoryEventProcessorFunction
    {
        private readonly ILogger<HotelCategoryEventProcessorFunction> _logger;
        private readonly IHotelCategoryEventProcessor _hotelCategoryEventProcessor;
       
        public HotelCategoryEventProcessorFunction(
            ILogger<HotelCategoryEventProcessorFunction> log, 
            IHotelCategoryEventProcessor hotelCategoryEventProcessor)
        {
            _logger = log;           
            _hotelCategoryEventProcessor = hotelCategoryEventProcessor;
        }

        
        [FunctionName("HotelCategoryEventProcessor")]
        public async Task HotelCategoryEventProcessorAsync([ServiceBusTrigger("hotel-category-events", "hotel-category-event-processor", Connection = "EventMessageConnectionString")] string @eventMessage)
        {
            try
            {
                _logger.LogInformation($"{nameof(HotelCategoryEventProcessorAsync)} function received message: {@eventMessage}");

                var @event = JsonSerializer.Deserialize<EventMessage<string>>(@eventMessage);

                await _hotelCategoryEventProcessor.ProcessHotelCategoryEventAsync(@event.EventType, @event.Payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(HotelCategoryEventProcessorAsync)} failed to process message: {@eventMessage}, Reason: {ex.Message}");
            }
        }
    }
}
