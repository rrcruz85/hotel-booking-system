using Hotel.Booking.Common.Contract.Messaging;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Reservation.Management.DataAccess.Interfaces;
using Reservation.Management.Model;
using Reservation.Management.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace Reservation.Management.Service.Tests.ReservationService
{
    [TestFixture]
    public class ReservationServiceTest
    {
        protected Mock<IReservationRuleService> _reservationRuleService;
        protected Mock<IReservationRepository> _reservationRepository;
        protected Mock<IReservationHistoryRepository> _reservationHistoryRepository;
        protected Mock<IRoomReservationRepository> _roomReservationRepository;
        protected Mock<IMessagingEngine> _messagingEngine;
        protected Mock<IConfiguration> _config;
        protected CreateUpdateReservation _reservationContext;
        protected DataAccess.Entities.Reservation _reservationEntity;
        protected IReservationService _sut;      

        [SetUp]
        public void Setup()
        {
            _reservationRuleService = new Mock<IReservationRuleService>();
            _reservationRepository = new Mock<IReservationRepository>();
            _reservationHistoryRepository = new Mock<IReservationHistoryRepository>();
            _roomReservationRepository = new Mock<IRoomReservationRepository>();
            _messagingEngine = new Mock<IMessagingEngine>();
            _config = new Mock<IConfiguration>();


            _sut = new  Implementations.ReservationService(
                _reservationRuleService.Object,
                _reservationRepository.Object,
                _reservationHistoryRepository.Object,
                _roomReservationRepository.Object,
                _messagingEngine.Object,
                _config.Object);

            _reservationContext = new CreateUpdateReservation
            {
                ReservationId = 1,
                StartDate = DateTime.Now.AddDays(3),
                EndDate = DateTime.Now.AddDays(6),
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

            _reservationEntity = new DataAccess.Entities.Reservation
            {
                Id = 1,
                StartDate = _reservationContext.StartDate,
                EndDate = _reservationContext.EndDate,
                Observations = _reservationContext.Observations,
                PaymentMethodInfo = _reservationContext.PaymentMethodInfo,
                PaymentMethodType = _reservationContext.PaymentMethodType,
                Status = _reservationContext.Status,
                UserId = _reservationContext.UserId,
                User = new DataAccess.Entities.User
                {
                    Id = 1,
                    UserName = "test-user"
                },
                RoomReservations = new List<DataAccess.Entities.RoomReservation>
                {
                    new DataAccess.Entities.RoomReservation
                    {
                        Id= 1,
                        Price= 100,
                        ReservationId = 1,
                        RoomId = 1
                    }
                },
                ReservationHistories = new List<DataAccess.Entities.ReservationHistory>
                {
                    new DataAccess.Entities.ReservationHistory
                    {
                        ReservationId = 1,
                        Id = 1,
                        CreatedDateTime = DateTime.Now,
                        Description  = "created",
                        Status = _reservationContext.Status,
                        UserId= _reservationContext.UserId,
                    }
                }
            };
        }
    }
}