using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Interfaces
{
    public interface IUserProfileEventProcessor
    {
        Task ProcessUserProfileEventAsync(int eventType, string eventPayload);
    }
}
