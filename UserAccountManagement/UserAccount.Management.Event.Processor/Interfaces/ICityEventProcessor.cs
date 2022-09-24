using System.Threading.Tasks;

namespace UserAccount.Management.Event.Processor.Interfaces
{
    public interface ICityEventProcessor
    {
        Task ProcessCityEventAsync(int eventType, string eventPayload);
    }
}
