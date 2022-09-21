namespace Hotel.Booking.Common.Constant
{
    public enum RoomStatus
    {
        Created = 1,
        Updated,
        Deleted,
        Available,
        Booked,
        OutOfService
    }

    public enum RoomType
    {
        Simple = 1,
        Double,
        Triple
    }
}