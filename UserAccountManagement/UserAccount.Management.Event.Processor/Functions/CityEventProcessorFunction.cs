using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Threading.Tasks;
using Hotel.Booking.Common.Contract.Messaging;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using UserAccount.Management.Event.Processor.Interfaces;

namespace UserAccount.Management.Event.Processor
{
    [ExcludeFromCodeCoverage]
    public class CityEventProcessorFunction
    {
        private readonly ILogger<CityEventProcessorFunction> _logger;
         
        private readonly ICityEventProcessor _cityEventProcessor;

        public CityEventProcessorFunction(
            ILogger<CityEventProcessorFunction> log,             
            ICityEventProcessor cityEventProcessor)
        {
            _logger = log;
            _cityEventProcessor = cityEventProcessor;
        }
 
        [FunctionName("CityEventProcessor")]
        public async Task CityEventProcessorAsync([ServiceBusTrigger("city-events", "city-user-account-event-processor", Connection = "EventMessageConnectionString")] string @eventMessage)
        {
            try
            {
                _logger.LogInformation($"{nameof(CityEventProcessorAsync)} function received message: {@eventMessage}");

                var @event = JsonSerializer.Deserialize<EventMessage<string>>(@eventMessage);

                await _cityEventProcessor.ProcessCityEventAsync(@event.EventType, @event.Payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CityEventProcessorAsync)} failed to process message: {@eventMessage}, Reason: {ex.Message}");
            }
        }
    }
}
