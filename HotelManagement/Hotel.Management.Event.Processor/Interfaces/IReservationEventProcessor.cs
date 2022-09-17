using System.Threading.Tasks;

namespace Hotel.Management.Event.Processor.Interfaces
{
    public interface IReservationEventProcessor
    {
        Task ProcessReservationEventAsync(int eventType, string eventPayload);
    }
}
