using System.Threading.Tasks;

namespace Hotel.Management.Event.Processor.Interfaces
{
    public interface IRoomEventProcessor
    {
        Task ProcessRoomEventAsync(int eventType, string eventPayload);
    }
}
