using Hotel.Booking.Common.Constant;
using Hotel.Management.WebApi.Models.Requests;

namespace Hotel.Management.WebApi.Translator
{
    public static class RoomTranslator
    {
        public static Model.Room ToBusinessModel(this CreateRoom model)
        {
            return new Model.Room
            {
               Type = model.Type,
               CurrentPrice = model.CurrentPrice,   
               Floor = model.Floor,
               HotelId = model.HotelId,
               MaxCapacity = model.MaxCapacity, 
               Number = model.Number,
               Status = (int)RoomStatus.Created
            };
        }

        public static Model.Room ToBusinessModel(this UpdateRoom model)
        {
            return new Model.Room
            {
                Id = model.Id,
                Type = model.Type,
                CurrentPrice = model.CurrentPrice,
                Floor = model.Floor,
                HotelId = model.HotelId,
                MaxCapacity = model.MaxCapacity,
                Number = model.Number,
                Status = model.Status
            };
        }
    }
}
