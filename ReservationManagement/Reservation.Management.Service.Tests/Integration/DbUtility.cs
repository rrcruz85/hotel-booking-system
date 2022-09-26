using Hotel.Booking.Common.Constant;
using Reservation.Management.DataAccess;
using Reservation.Management.DataAccess.Entities;
using System.Collections.Generic;

namespace Reservation.Management.Service.Tests.Integration
{
    public static class DbUtility
    {
        public static void InitializeDbForTests(ReservationManagementContext db)
        {
            db.Cities.AddRange(GetSeedingCities());
            db.Hotels.AddRange(GetSeedingHotels());
            db.Rooms.AddRange(GetSeedingRooms());
            db.Roles.AddRange(GetSeedingRoles());
            db.Users.AddRange(GetSeedingUsers());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ReservationManagementContext db)
        {
            db.Cities.RemoveRange(db.Cities);
            db.Hotels.RemoveRange(db.Hotels);
            db.Rooms.RemoveRange(db.Rooms);
            db.Roles.RemoveRange(db.Roles);
            db.Users.RemoveRange(db.Users);
            InitializeDbForTests(db);
        }

        public static List<City> GetSeedingCities()
        {
            return new List<City>()
            {
                new City(){ Country = "Mexico", State = "Merida", Name = "Cancun" }
            };
        }

        public static List<DataAccess.Entities.Hotel> GetSeedingHotels()
        {
            return new List<DataAccess.Entities.Hotel>()
            {
                new DataAccess.Entities.Hotel {Name = "Grand Caribbean", CityId = 1, AddressLine1 = "Street 1"}
            };
        }

        public static List<Room> GetSeedingRooms()
        {
            return new List<Room>()
            {
                new Room {CurrentPrice = 100, Floor = 1, HotelId = 1, MaxCapacity = 4, Number = 101, Status = (int)RoomStatus.Available, Type = (int)RoomType.Double},
                new Room {CurrentPrice = 50,  Floor = 1, HotelId = 1, MaxCapacity = 2, Number = 102, Status = (int)RoomStatus.Available, Type = (int)RoomType.Simple},
                new Room {CurrentPrice = 150, Floor = 1, HotelId = 1, MaxCapacity = 4, Number = 103, Status = (int)RoomStatus.Available, Type = (int)RoomType.Double},
                new Room {CurrentPrice = 200, Floor = 1, HotelId = 1, MaxCapacity = 4, Number = 104, Status = (int)RoomStatus.Available, Type = (int)RoomType.Triple},
            };
        }

        public static List<Role> GetSeedingRoles()
        {
            return new List<Role>()
            {
                new Role { Name = "Customer"},
                new Role { Name = "Manager"},
            };
        }

        public static List<User> GetSeedingUsers()
        {
            return new List<User>()
            {
                new User {IsActive = true, Passoword = "123456", UserName = "TestUser", RoleId = 1}
            };
        }
    }
}
