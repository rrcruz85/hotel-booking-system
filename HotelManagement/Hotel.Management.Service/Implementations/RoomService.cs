using Hotel.Booking.Common.Constant;
using Hotel.Booking.Common.Contract.Exceptions;
using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Booking.Common.Contract.Services;
using Hotel.Management.DataAccess.Interfaces;
using Hotel.Management.Model;
using Hotel.Management.Service.Interfaces;
using Hotel.Management.Service.Translators;
using System.Diagnostics.CodeAnalysis;

namespace Hotel.Management.Service.Implementations
{
    [ExcludeFromCodeCoverage]
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMessagingEngine _messagingEngine;
        private readonly IConfigurationView _config;
        private readonly string TopicName = "RoomTopicName";

        public RoomService(
            IRoomRepository roomRepository, 
            IMessagingEngine messagingEngine, 
            IConfigurationView config)
        {
            _roomRepository = roomRepository;
            _messagingEngine = messagingEngine;
            _config = config;
        }

        public async Task<int> CreateRoomAsync(Room room)
        {
            if (await _roomRepository.AnyAsync(r => r.HotelId == room.HotelId && r.Number == room.Number))
            {
                throw new InvalidValueException($"Room number can not be duplicated");
            }
            var newEntity = room.ToNewEntity();
            newEntity.Status = (int)RoomStatus.Available;
            var roomId = await _roomRepository.AddAsync(newEntity);
            newEntity.Id = roomId;
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)RoomEventType.Created, newEntity);
            return roomId;
        }

        public async Task DeleteRoomAsync(int roomId)
        {
            var room = await _roomRepository.SingleOrDefaultAsync(c => c.Id == roomId);
            if (room == null)
            {
                throw new ArgumentException($"Room {roomId} does not exist");
            }
            await _roomRepository.DeleteAsync(room);
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)RoomEventType.Deleted, roomId);
        }

        public async Task<List<Room>> GetAllRoomsByHotelIdAsync(int hotelId)
        {
            var rooms = await _roomRepository.WhereAsync(c => c.HotelId == hotelId);
            return rooms.Select(r => r.ToModel()).OrderBy(r => r.Number).ToList();
        }

        public async Task<Room?> GetRoomByAsync(int roomId)
        {
            var room = await _roomRepository.SingleOrDefaultAsync(c => c.Id == roomId);
            return room?.ToModel();
        }

        public async Task<Room?> GetRoomByNumberAsync(int hotelId, int number)
        {
            var room = await _roomRepository.SingleOrDefaultAsync(r => r.HotelId == hotelId && r.Number == number);
            return room?.ToModel();
        }

        public async Task<List<Room>> GetRoomsByStatusAndHotelAsync(int hotelId, int status)
        {
            var rooms = await _roomRepository.WhereAsync(r => r.Status == status && r.HotelId == hotelId);
            return rooms.Select(r => r.ToModel()).OrderBy(r => r.Number).ToList();
        }

        public async Task<List<Room>> GetRoomsByTypeAndHotelAsync(int hotelId, int type)
        {
            var rooms = await _roomRepository.WhereAsync(r => r.Type == type && r.HotelId == hotelId);
            return rooms.Select(r => r.ToModel()).OrderBy(r => r.Number).ToList();
        }

        public IDictionary<int, string> GetRoomStatuses()
        {
            return new Dictionary<int, string>
            {
                {(int)RoomStatus.Created,  RoomStatus.Created.ToString()},
                {(int)RoomStatus.Updated,  RoomStatus.Updated.ToString()},
                {(int)RoomStatus.Deleted,  RoomStatus.Deleted.ToString()},
                {(int)RoomStatus.Available,  RoomStatus.Available.ToString()},
                {(int)RoomStatus.OutOfService,  RoomStatus.OutOfService.ToString()},
                {(int)RoomStatus.Booked,  RoomStatus.Booked.ToString()},
            };
        }

        public IDictionary<int, string> GetRoomTypes()
        {
            return new Dictionary<int, string>
            {
                {(int)RoomType.Simple,  RoomType.Simple.ToString()},
                {(int)RoomType.Double,  RoomType.Double.ToString()},
                {(int)RoomType.Triple,  RoomType.Triple.ToString()} 
            };
        }

        public async Task UpdateRoomAsync(Room room)
        {
            var entity = await _roomRepository.SingleOrDefaultAsync(c => c.Id == room.Id);
            if (entity == null)
            {
                throw new InvalidValueException($"Room {room.Id} does not exist");
            }
            if (await _roomRepository.AnyAsync(r => r.Id != room.Id && r.Number == room.Number && r.HotelId == room.HotelId))
            {
                throw new InvalidValueException($"Room number can not be duplicated");
            }
            await _roomRepository.UpdateAsync(room.ToEntity());
            await _messagingEngine.PublishEventMessageAsync(_config.AppSettings(TopicName), (int)RoomEventType.Updated, room);
        }

        public async Task UpdateRoomStatusAsync(int roomId, int status)
        {
            var entity = await _roomRepository.SingleOrDefaultAsync(c => c.Id == roomId);
            if (entity == null)
            {
                throw new InvalidValueException($"Room {roomId} does not exist");
            }
            entity.Status = status;
            await _roomRepository.UpdateAsync(entity);            
        }
    }
}
