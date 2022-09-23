using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Interfaces
{
    public interface ICityEventProcessor
    {
        Task ProcessCityEventAsync(int eventType, string eventPayload);
    }
}
