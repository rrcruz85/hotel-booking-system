
namespace Hotel.Booking.Common.Constant
{
    public enum CityEventType
    {
        Created = 1,
        Updated,
        Deleted
    }

    public enum HotelEventType
    {
        Created = 1,
        Updated,
        Deleted
    }

    public enum RoomEventType
    {
        Created = 1,
        Updated,
        Deleted,
        Available,
        Booked,
        OutOfService
    }

    public enum ReservationEventType
    {
        Created = 1,
        Updated,
        Deleted,
        Canceled,
        Payed
    }
}
