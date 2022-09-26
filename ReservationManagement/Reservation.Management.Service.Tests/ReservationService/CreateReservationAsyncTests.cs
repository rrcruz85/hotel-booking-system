using Hotel.Booking.Common.Constant;
using Moq;
using NUnit.Framework;
using Reservation.Management.Model;
using Reservation.Management.Model.Event;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reservation.Management.Service.Tests.ReservationService
{
    [TestFixture]
    public class CreateReservationAsyncTests : ReservationServiceTest
    {
        [Test]
        public void When_ValidationsFail_Then_Exception_IsExpected()
        {
            var failedValidation = new ReservationRuleValidationResponse()
            {
                Ok = false
            };

            _reservationRuleService.Setup(m => m.CheckRulesOnCreateAsync(It.IsAny<IReservationContext>())).ReturnsAsync(failedValidation);

            Assert.ThrowsAsync<ArgumentException>(() => _sut.CreateReservationAsync(_reservationContext));             
        }


        [Test]
        public async Task When_ValidationsAreOk_Then_ReservationIsCreated_HistoryRecordIsCreated_AndBookedRoomEventsAreFired()
        {
            var okValidation = new ReservationRuleValidationResponse()
            {
                Ok = true
            };
            _config.Setup(m => m.AppSettings("RoomTopicName")).Returns("room-events");
            _reservationRuleService.Setup(m => m.CheckRulesOnCreateAsync(It.IsAny<IReservationContext>())).ReturnsAsync(okValidation);
            _reservationRepository.Setup(m => m.AddAsync(It.IsAny<DataAccess.Entities.Reservation>())).ReturnsAsync(_reservationContext.ReservationId);
            _roomReservationRepository.Setup(m => m.AddMultipleAsync(It.IsAny<List<DataAccess.Entities.RoomReservation>>())).Verifiable();
            _reservationHistoryRepository.Setup(m => m.AddAsync(It.IsAny<DataAccess.Entities.ReservationHistory>())).Verifiable();
            _messagingEngine.Setup(m => m.PublishEventMessageAsync("room-events", (int)RoomEventType.Booked, It.IsAny<RoomStatusEvent>())).Verifiable();

            var result = await _sut.CreateReservationAsync(_reservationContext);

            Assert.That(result == _reservationContext.ReservationId);

            _reservationRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.Reservation>()), Times.Once);
            _roomReservationRepository.Verify(m => m.AddMultipleAsync(It.IsAny<List<DataAccess.Entities.RoomReservation>>()), Times.Once);
            _reservationHistoryRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.ReservationHistory>()), Times.Once);
            _messagingEngine.Verify(m => m.PublishEventMessageAsync("room-events", (int)RoomEventType.Booked, It.IsAny<RoomStatusEvent>()), Times.AtLeastOnce);
        }

    }
}
