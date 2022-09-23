
namespace Reservation.Management.Model.Event
{
    public class RoomStatusEvent
    {
        public int RoomId { get; set; }
        public int Status { get; set; }
    }
}
