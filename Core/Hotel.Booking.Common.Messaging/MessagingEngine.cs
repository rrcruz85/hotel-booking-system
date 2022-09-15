
using System.Text.Json;

namespace Hotel.Booking.Common.Messaging
{
    public static class MessagingEngine
    {
        public static async Task PublishEventMessageAsync<T>(string topic, int eventType, T payload)
        {
            var @event = new EventMessage<T>
            {
                EventType = eventType,
                Payload = payload
            };
            await ServiceBusMessagingBroker.SendMessageAsync(topic, JsonSerializer.Serialize(@event));
        }
        
        public static async Task SheduledEventMessageMessageAsync<T>(string topic, int eventType, T payload, DateTimeOffset scheduleTime)
        {
            var @event = new EventMessage<T>
            {
                EventType = eventType,
                Payload = payload
            };
            await ServiceBusMessagingBroker.SendSheduledMessageAsync(topic, JsonSerializer.Serialize(@event), scheduleTime);
        }
    }
}
