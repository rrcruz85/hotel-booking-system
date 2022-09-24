using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Interfaces
{
    public interface IUserEventProcessor
    {
        Task ProcessUserEventAsync(int eventType, string eventPayload);
    }
}
