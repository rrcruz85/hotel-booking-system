using Hotel.Management.DataAccess.Entities;

namespace Hotel.Management.Service.Translators
{
    public static class RoomTranslator
    {
        public static Room ToNewEntity(this Model.Room room)
        {
            return new Room
            {                
               HotelId = room.HotelId,
               Number = room.Number,
               Floor = room.Floor,
               CurrentPrice = room.CurrentPrice,              
               MaxCapacity = room.MaxCapacity,
               Status = room.Status,
               Type = room.Type
            };
        }

        public static Room ToEntity(this Model.Room room)
        {
            var entity = room.ToNewEntity();
            entity.Id = room.Id;
            return entity;
        }

        public static Model.Room ToModel(this Room room)
        {
            return new Model.Room
            {
               CurrentPrice = room.CurrentPrice,               
               Floor = room.Floor,
               HotelId = room.HotelId,
               Id = room.Id,
               MaxCapacity = room.MaxCapacity,
               Type = room.Type,
               Status = room.Status,
               Number = room.Number
            };
        }
    }
}
