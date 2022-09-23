using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Interfaces
{
    public interface IRoomEventProcessor
    {
        Task ProcessRoomEventAsync(int eventType, string eventPayload);
    }
}
