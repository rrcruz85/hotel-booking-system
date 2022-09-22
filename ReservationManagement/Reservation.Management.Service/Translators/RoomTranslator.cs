using Reservation.Management.DataAccess.Entities;

namespace Reservation.Management.Service.Translators
{
    public static class RoomTranslator
    {
        public static Room ToEntity(this Model.Room room)
        {
            return new Room
            {
                Id = room.Id,
                HotelId = room.HotelId,
                Number = room.Number,
                Floor = room.Floor,
                CurrentPrice = room.CurrentPrice,
                MaxCapacity = room.MaxCapacity,
                Status = room.Status,
                Type = room.Type
            };
        }         

        public static Model.Room ToModel(this Room room)
        {
            return new Model.Room
            {
                Id = room.Id,
                CurrentPrice = room.CurrentPrice,
                Floor = room.Floor,
                HotelId = room.HotelId,
                MaxCapacity = room.MaxCapacity,
                Type = room.Type,
                Status = room.Status,
                Number = room.Number
            };
        }
    }
}
