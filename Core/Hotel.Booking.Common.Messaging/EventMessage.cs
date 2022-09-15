
namespace Hotel.Booking.Common.Messaging
{
    public class EventMessage<T>
    {
        public T? Payload { get; set; }
        public int EventType { get; set; }
    }
}
