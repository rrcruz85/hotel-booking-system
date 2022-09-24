
namespace Hotel.Booking.Common.Constant
{
    public enum CityEventType
    {
        Created = 1,
        Updated,
        Deleted
    }

    public enum HotelCategoryEventType
    {
        Created = 1,
        Updated,
        Deleted
    }

    public enum HotelCategoryRelationEventType
    {
        Created = 1,
        Deleted
    }

    public enum HotelContactInfoEventType
    {
        Created = 1,
        Updated,
        Deleted
    }

    public enum HotelFacilityEventType
    {
        Created = 1,
        Updated,
        Deleted
    }

    public enum HotelImageEventType
    {
        Created = 1,
        Updated,
        Deleted
    }

    public enum HotelServiceEventType
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

    public enum UserEventType
    {
        Created = 1,
        Updated,
        Deleted
    }

    public enum UserProfileEventType
    {
        Created = 1,
        Updated,
        Deleted
    }

    public enum UserContactInfoEventType
    {
        Created = 1,
        Updated,
        Deleted
    }

    public enum RoleEventType
    {
        Created = 1,
        Updated,
        Deleted
    }

    public enum RolePermissionEventType
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
