using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Interfaces
{
    public interface IRoleEventProcessor
    {
        Task ProcessRoleEventAsync(int eventType, string eventPayload);
    }
}
