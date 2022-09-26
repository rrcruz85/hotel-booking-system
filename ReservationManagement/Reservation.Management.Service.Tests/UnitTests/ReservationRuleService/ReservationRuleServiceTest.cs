using Moq;
using NUnit.Framework;
using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace Reservation.Management.Service.Tests.ReservationRuleService
{
    [TestFixture]
    public class ReservationRuleServiceTest
    {
        protected Mock<IRoomRepository> _roomRepository;
        protected CreateUpdateReservation _reservationContext;
        protected IReservationRuleService _sut;      

        [SetUp]
        public void Setup()
        {
            _roomRepository = new Mock<IRoomRepository>();
            _sut = new Implementations.ReservationRuleService(_roomRepository.Object);
            _reservationContext = new CreateUpdateReservation
            {
                ReservationId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                HotelId = 1,
                Observations = string.Empty,
                PaymentMethodInfo = "1234",
                PaymentMethodType = 1,
                Status = 1,
                UserId = 1,
                Rooms = new List<CreateUpdateRoomReservation>
                {
                    new CreateUpdateRoomReservation 
                    {
                        RoomId = 1,
                        Price = 100
                    }
                }
            };
        }
    }
}