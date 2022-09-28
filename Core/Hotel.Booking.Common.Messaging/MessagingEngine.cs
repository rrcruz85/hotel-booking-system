﻿
using Hotel.Booking.Common.Contract.Messaging;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Hotel.Booking.Common.Messaging
{
    [ExcludeFromCodeCoverage]
    public class MessagingEngine : IMessagingEngine
    {
        private readonly IMessagingBroker _messagingBroker;

        public MessagingEngine(IMessagingBroker messagingBroker)
        {
            _messagingBroker = messagingBroker;
        }

        public async Task PublishEventMessageAsync<T>(string queueOrTopicName, int eventType, T payload)
        {
            var @event = new EventMessage<string>
            {
                EventType = eventType,
                Payload = JsonSerializer.Serialize(payload)
            };
            await _messagingBroker.SendMessageAsync(queueOrTopicName, JsonSerializer.Serialize(@event));
        }

        public async Task SheduledEventMessageMessageAsync<T>(string queueOrTopicName, int eventType, T payload, DateTimeOffset scheduleTime)
        {
            var @event = new EventMessage<string>
            {
                EventType = eventType,
                Payload = JsonSerializer.Serialize(payload)
            };
            await _messagingBroker.SendSheduledMessageAsync(queueOrTopicName, JsonSerializer.Serialize(@event), scheduleTime);
        }
    }
}
