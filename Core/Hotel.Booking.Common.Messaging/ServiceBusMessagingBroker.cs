using Azure.Messaging.ServiceBus;
using Hotel.Booking.Common.Contract.Messaging;
using Microsoft.Extensions.Configuration;

namespace Hotel.Booking.Common.Messaging
{
    public class ServiceBusMessagingBroker : IMessagingBroker
    {
        private readonly IConfiguration _config;

        public ServiceBusMessagingBroker(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMessageAsync(string topicName, string message)
        {
            await using var client = new ServiceBusClient(_config["EventMessageConnectionString"]);
            var sender = client.CreateSender(topicName);
            var serviceBusMessage = new ServiceBusMessage(message);
            await sender.SendMessageAsync(serviceBusMessage);
        }

        public async Task SendMessageAsync(string topicName, string message, IDictionary<string, object> messageProperties)
        {
            await using var client = new ServiceBusClient(_config["EventMessageConnectionString"]);
            var sender = client.CreateSender(topicName);
            var serviceBusMessage = new ServiceBusMessage(message);

            if (messageProperties != null && messageProperties.Keys.Any())
            {
                foreach (var key in messageProperties.Keys)
                {
                    serviceBusMessage.ApplicationProperties.Add(key, messageProperties[key]);
                }
            }
            await sender.SendMessageAsync(serviceBusMessage);
        }

        public async Task<long> SendSheduledMessageAsync(string topicName, string message, DateTimeOffset sheduledEnqueueTime)
        {
            await using var client = new ServiceBusClient(_config["EventMessageConnectionString"]);
            var sender = client.CreateSender(topicName);
            var serviceBusMessage = new ServiceBusMessage(message);
            return await sender.ScheduleMessageAsync(serviceBusMessage, sheduledEnqueueTime);
        }
    }
}
