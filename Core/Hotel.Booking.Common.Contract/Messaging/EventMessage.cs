namespace Hotel.Booking.Common.Contract.Messaging
{
    public sealed class EventMessage<T>
    {
        public T? Payload { get; set; }
        public int EventType { get; set; }
    }
}
