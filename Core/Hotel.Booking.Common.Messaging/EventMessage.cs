
namespace Hotel.Booking.Common.Messaging
{
    internal sealed class EventMessage<T>
    {
        public T? Payload { get; set; }
        public int EventType { get; set; }
    }
}
