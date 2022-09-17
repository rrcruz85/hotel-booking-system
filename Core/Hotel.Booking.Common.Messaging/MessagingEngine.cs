
using Hotel.Booking.Common.Contract.Messaging;
using System.Text.Json;

namespace Hotel.Booking.Common.Messaging
{
    public class MessagingEngine : IMessagingEngine
    {
        private readonly IMessagingBroker _messagingBroker;

        public MessagingEngine(IMessagingBroker messagingBroker)
        {
            _messagingBroker = messagingBroker;
        }

        public async Task PublishEventMessageAsync<T>(string queueOrTopicName, int eventType, T payload)
        {
            var @event = new EventMessage<T>
            {
                EventType = eventType,
                Payload = payload
            };
            await _messagingBroker.SendMessageAsync(queueOrTopicName, JsonSerializer.Serialize(@event));
        }

        public async Task SheduledEventMessageMessageAsync<T>(string queueOrTopicName, int eventType, T payload, DateTimeOffset scheduleTime)
        {
            var @event = new EventMessage<T>
            {
                EventType = eventType,
                Payload = payload
            };
            await _messagingBroker.SendSheduledMessageAsync(queueOrTopicName, JsonSerializer.Serialize(@event), scheduleTime);
        }
    }
}
