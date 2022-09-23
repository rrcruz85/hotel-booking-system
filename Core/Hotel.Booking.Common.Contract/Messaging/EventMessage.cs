using System.Diagnostics.CodeAnalysis;

namespace Hotel.Booking.Common.Contract.Messaging
{
    [ExcludeFromCodeCoverage]
    public sealed class EventMessage<T>
    {
        public T? Payload { get; set; }
        public int EventType { get; set; }
    }
}
