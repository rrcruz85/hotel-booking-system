
namespace Hotel.Booking.Common.Contract.Messasing
{
    public interface IMessagingBroker
    {
        Task SendMessageAsync(string queueOrTopicName, string message);
        Task SendMessageAsync(string queueOrTopicName, string message, IDictionary<string, object> messageProperties);
        Task<long> SendSheduledMessageAsync(string queueOrTopicName, string message, DateTimeOffset sheduledEnqueueTime);
    }
}