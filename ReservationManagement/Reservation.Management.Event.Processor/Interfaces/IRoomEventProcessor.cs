using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Interfaces
{
    public interface IHotelEventProcessor
    {
        Task ProcessHotelEventAsync(int eventType, string eventPayload);
    }
}
