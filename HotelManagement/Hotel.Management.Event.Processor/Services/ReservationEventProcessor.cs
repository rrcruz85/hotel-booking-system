using System.Threading.Tasks;

namespace Hotel.Management.Event.Processor.Interfaces
{
    public class ReservationEventProcessor: IReservationEventProcessor
    {        
        public async Task ProcessReservationEventAsync(int eventType, string eventPayload)
        {
            throw new System.NotImplementedException();
        }
    }
}
