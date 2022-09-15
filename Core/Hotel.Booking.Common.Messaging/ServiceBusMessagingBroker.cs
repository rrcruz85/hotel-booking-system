using Azure.Messaging.ServiceBus;
using Hotel.Booking.Common.Utility;

namespace Hotel.Booking.Common.Messaging
{
    internal static class ServiceBusMessagingBroker
    {
        private static readonly string ServiceBusConnectionString = Configuration.AppSettings("EventMessageConnectionString");

        public static async Task SendMessageAsync(string topicName, string message)
        {
            await using var client = new ServiceBusClient(ServiceBusConnectionString);
            var sender = client.CreateSender(topicName);
            var serviceBusMessage = new ServiceBusMessage(message);
            await sender.SendMessageAsync(serviceBusMessage);
        }

        public static async Task SendMessageAsync(string topicName, string message, IDictionary<string, object> messageProperties)
        {
            await using var client = new ServiceBusClient(ServiceBusConnectionString);
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
        
        public static async Task<long> SendSheduledMessageAsync(string topicName, string message, DateTimeOffset sheduledEnqueueTime)
        {
            await using var client = new ServiceBusClient(ServiceBusConnectionString);
            var sender = client.CreateSender(topicName);
            var serviceBusMessage = new ServiceBusMessage(message);
            return await sender.ScheduleMessageAsync(serviceBusMessage, sheduledEnqueueTime);
        }
    }
}
