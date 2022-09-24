using System.Threading.Tasks;

namespace Reservation.Management.Event.Processor.Interfaces
{
    public interface IHotelCategoryEventProcessor
    {
        Task ProcessHotelCategoryEventAsync(int eventType, string eventPayload);
    }
}
