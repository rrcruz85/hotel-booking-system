
namespace Hotel.Booking.Common.Contract.Messaging
{
    public interface IMessagingEngine
    {
        Task PublishEventMessageAsync<T>(string queueOrTopicName, int eventType, T payload);
        Task SheduledEventMessageMessageAsync<T>(string queueOrTopicName, int eventType, T payload, DateTimeOffset scheduleTime);
    }
}