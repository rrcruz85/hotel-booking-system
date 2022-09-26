using Hotel.Booking.Common.Constant;
using Moq;
using NUnit.Framework;
using Reservation.Management.Model;
using Reservation.Management.Model.Event;
using System;
using System.Collections.Generic;

namespace Reservation.Management.Service.Tests.ReservationService
{
    [TestFixture]
    public class UpdateReservationAsyncTests : ReservationServiceTest
    {

        [Test]
        public void When_ReservationDoesNotExist_Then_Exception_IsExpected()
        {
            _reservationRepository.Setup(m => m.GetWithRelationsAsync(_reservationContext.ReservationId)).ReturnsAsync(value: null);

            Assert.ThrowsAsync<ArgumentException>(() => _sut.UpdateReservationAsync(_reservationContext));
        }

        [Test]
        public void When_ValidationsFail_Then_Exception_IsExpected()
        {
            var failedValidation = new ReservationRuleValidationResponse()
            {
                Ok = false
            };

            _reservationRuleService.Setup(m => m.CheckRulesOnUpdateAsync(It.IsAny<IReservationContext>())).ReturnsAsync(failedValidation);

            Assert.ThrowsAsync<ArgumentException>(() => _sut.UpdateReservationAsync(_reservationContext));             
        }

        [Test]
        public void When_ReservationIsCanceled_Then_Exception_IsExpected()
        {
            var okValidation = new ReservationRuleValidationResponse()
            {
                Ok = true
            };

            _reservationEntity.Status = (int)ReservationStatus.Canceled;
            _reservationRepository.Setup(m => m.GetWithRelationsAsync(_reservationContext.ReservationId)).ReturnsAsync(_reservationEntity);
            _reservationRuleService.Setup(m => m.CheckRulesOnUpdateAsync(It.IsAny<IReservationContext>())).ReturnsAsync(okValidation);

            Assert.ThrowsAsync<ArgumentException>(() => _sut.UpdateReservationAsync(_reservationContext));
        }

        [Test]
        public void When_OnGoingReservation_AndStartDateIsChanged_Then_Exception_IsExpected()
        {
            var okValidation = new ReservationRuleValidationResponse()
            {
                Ok = true
            };

            _reservationEntity.StartDate = DateTime.Now.AddDays(-1);

            _reservationRepository.Setup(m => m.GetWithRelationsAsync(_reservationContext.ReservationId)).ReturnsAsync(_reservationEntity);
            _reservationRuleService.Setup(m => m.CheckRulesOnUpdateAsync(It.IsAny<IReservationContext>())).ReturnsAsync(okValidation);
            _reservationContext.StartDate = DateTime.Now;

            Assert.ThrowsAsync<ArgumentException>(() => _sut.UpdateReservationAsync(_reservationContext));
        }

        [Test]
        public void When_ValidationsAreOk_AndStatusHasNotChanged_AndRoomsEither_Then_ReservationIsUpdated_HistoryRecordIsNotCreated_AndBookedRoomEventsAreNotFired()
        {
            var okValidation = new ReservationRuleValidationResponse()
            {
                Ok = true
            };
            _config.Setup(m => m.AppSettings("RoomTopicName")).Returns("room-events");

            _reservationRepository.Setup(m => m.GetWithRelationsAsync(It.IsAny<int>())).ReturnsAsync(_reservationEntity);

            Assert.DoesNotThrowAsync(() => _sut.UpdateReservationAsync(_reservationContext));

            _reservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.Reservation>()), Times.Once);
            _roomReservationRepository.Verify(m => m.DeleteMultipleAsync(It.IsAny<List<DataAccess.Entities.RoomReservation>>()), Times.Never);
            _roomReservationRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _roomReservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _reservationHistoryRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.ReservationHistory>()), Times.Never);
            _messagingEngine.Verify(m => m.PublishEventMessageAsync("room-events", (int)RoomEventType.Booked, It.IsAny<RoomStatusEvent>()), Times.Never);
        }

        [Test]
        public void When_ValidationsAreOk_AndStatusHasChanged_AndRoomsHasNotChanged_Then_ReservationIsUpdated_HistoryRecordIsCreated_AndBookedRoomEventsAreNotFired()
        {
            var okValidation = new ReservationRuleValidationResponse()
            {
                Ok = true
            };
            _config.Setup(m => m.AppSettings("RoomTopicName")).Returns("room-events");

            _reservationEntity.Status = (int)ReservationStatus.Booked;
            _reservationRepository.Setup(m => m.GetWithRelationsAsync(It.IsAny<int>())).ReturnsAsync(_reservationEntity);

            _reservationContext.Status = (int)ReservationStatus.OnGoing;

            Assert.DoesNotThrowAsync(() => _sut.UpdateReservationAsync(_reservationContext));

            _reservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.Reservation>()), Times.Once);
            _roomReservationRepository.Verify(m => m.DeleteMultipleAsync(It.IsAny<List<DataAccess.Entities.RoomReservation>>()), Times.Never);
            _roomReservationRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _roomReservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _reservationHistoryRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.ReservationHistory>()), Times.Once);
            _messagingEngine.Verify(m => m.PublishEventMessageAsync("room-events", (int)RoomEventType.Booked, It.IsAny<RoomStatusEvent>()), Times.Never);
        }

        [Test]
        public void When_ValidationsAreOk_AndStatusHasNotChanged_AndNewRoomsHaveBeenCreated_Then_ReservationIsUpdated_HistoryRecordIsNotCreated_AndBookedRoomEventsAreFired()
        {
            var okValidation = new ReservationRuleValidationResponse()
            {
                Ok = true
            };
            _reservationRuleService.Setup(m => m.CheckRulesOnUpdateAsync(It.IsAny<IReservationContext>())).ReturnsAsync(okValidation);

            _config.Setup(m => m.AppSettings("RoomTopicName")).Returns("room-events");

            _reservationRepository.Setup(m => m.GetWithRelationsAsync(It.IsAny<int>())).ReturnsAsync(_reservationEntity);

            _reservationContext.Rooms.Add(new CreateUpdateRoomReservation
            {
                RoomId = 2,
                Price = 200
            });

            Assert.DoesNotThrowAsync(() => _sut.UpdateReservationAsync(_reservationContext));

            _reservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.Reservation>()), Times.Once);
            _roomReservationRepository.Verify(m => m.DeleteMultipleAsync(It.IsAny<List<DataAccess.Entities.RoomReservation>>()), Times.Never);
            _roomReservationRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Once);
            _roomReservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _reservationHistoryRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.ReservationHistory>()), Times.Never);
            _messagingEngine.Verify(m => m.PublishEventMessageAsync("room-events", (int)RoomEventType.Booked, It.IsAny<RoomStatusEvent>()), Times.AtLeastOnce);
        }

        [Test]
        public void When_ValidationsAreOk_AndStatusHasNotChanged_AndOldRoomsHaveBeenRemoved_Then_ReservationIsUpdated_HistoryRecordIsNotCreated_AndBookedRoomEventsAreFired()
        {
            var okValidation = new ReservationRuleValidationResponse()
            {
                Ok = true
            };

            _reservationRuleService.Setup(m => m.CheckRulesOnUpdateAsync(It.IsAny<IReservationContext>())).ReturnsAsync(okValidation);

            _config.Setup(m => m.AppSettings("RoomTopicName")).Returns("room-events");

            _reservationEntity.RoomReservations.Add(new DataAccess.Entities.RoomReservation
            {
                RoomId = 2,
                Price = 200
            });

            _reservationRepository.Setup(m => m.GetWithRelationsAsync(It.IsAny<int>())).ReturnsAsync(_reservationEntity);

            Assert.DoesNotThrowAsync(() => _sut.UpdateReservationAsync(_reservationContext));

            _reservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.Reservation>()), Times.Once);
            _roomReservationRepository.Verify(m => m.DeleteMultipleAsync(It.IsAny<List<DataAccess.Entities.RoomReservation>>()), Times.Once);
            _roomReservationRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _roomReservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _reservationHistoryRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.ReservationHistory>()), Times.Never);
            _messagingEngine.Verify(m => m.PublishEventMessageAsync("room-events", (int)RoomEventType.Available, It.IsAny<RoomStatusEvent>()), Times.AtLeastOnce);
        }


        [Test]
        public void When_ValidationsFailed_AndStatusHasNotChanged_AndRoomsHasChanged_Then_ReservationIsUpdated_HistoryRecordIsNotCreated_AndBookedRoomEventsAreFired()
        {
            var failedValidation = new ReservationRuleValidationResponse()
            {
                Ok = false
            };
            _reservationRuleService.Setup(m => m.CheckRulesOnUpdateAsync(It.IsAny<IReservationContext>())).ReturnsAsync(failedValidation);

            _config.Setup(m => m.AppSettings("RoomTopicName")).Returns("room-events");

            _reservationRepository.Setup(m => m.GetWithRelationsAsync(It.IsAny<int>())).ReturnsAsync(_reservationEntity);

            _reservationContext.Rooms.Add(new CreateUpdateRoomReservation
            {
                RoomId = 2,
                Price = 200
            });

            Assert.ThrowsAsync<ArgumentException>(() => _sut.UpdateReservationAsync(_reservationContext));

            _reservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.Reservation>()), Times.Never);
            _roomReservationRepository.Verify(m => m.DeleteMultipleAsync(It.IsAny<List<DataAccess.Entities.RoomReservation>>()), Times.Never);
            _roomReservationRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _roomReservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _reservationHistoryRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.ReservationHistory>()), Times.Never);
            _messagingEngine.Verify(m => m.PublishEventMessageAsync("room-events", (int)RoomEventType.Booked, It.IsAny<RoomStatusEvent>()), Times.Never);
        }

        [Test]
        public void When_ValidationsFailed_AndStatusHasNotChanged_AndRoomsHasNotChanged_AndDatesChanged_Then_ReservationIsUpdated_HistoryRecordIsNotCreated_AndBookedRoomEventsAreFired()
        {
            var failedValidation = new ReservationRuleValidationResponse()
            {
                Ok = false
            };
            _reservationRuleService.Setup(m => m.CheckRulesOnUpdateAsync(It.IsAny<IReservationContext>())).ReturnsAsync(failedValidation);

            _config.Setup(m => m.AppSettings("RoomTopicName")).Returns("room-events");

            _reservationRepository.Setup(m => m.GetWithRelationsAsync(It.IsAny<int>())).ReturnsAsync(_reservationEntity);

            _reservationContext.StartDate = DateTime.Now.AddDays(5);

            Assert.ThrowsAsync<ArgumentException>(() => _sut.UpdateReservationAsync(_reservationContext));

            _reservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.Reservation>()), Times.Never);
            _roomReservationRepository.Verify(m => m.DeleteMultipleAsync(It.IsAny<List<DataAccess.Entities.RoomReservation>>()), Times.Never);
            _roomReservationRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _roomReservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _reservationHistoryRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.ReservationHistory>()), Times.Never);
            _messagingEngine.Verify(m => m.PublishEventMessageAsync("room-events", (int)RoomEventType.Booked, It.IsAny<RoomStatusEvent>()), Times.Never);
        }

        [Test]
        public void When_ValidationsAreOk_AndStatusHasNotChanged_AndOneRoomHasUpdatedItsPrice_Then_ReservationIsUpdated_HistoryRecordIsNotCreated_AndBookedRoomEventsAreFired()
        {
            var okValidation = new ReservationRuleValidationResponse()
            {
                Ok = true
            };

            _reservationRuleService.Setup(m => m.CheckRulesOnUpdateAsync(It.IsAny<IReservationContext>())).ReturnsAsync(okValidation);

            _config.Setup(m => m.AppSettings("RoomTopicName")).Returns("room-events");

            _reservationRepository.Setup(m => m.GetWithRelationsAsync(It.IsAny<int>())).ReturnsAsync(_reservationEntity);

            _reservationContext.Rooms[0].Price = 200;

            Assert.DoesNotThrowAsync(() => _sut.UpdateReservationAsync(_reservationContext));

            _reservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.Reservation>()), Times.Once);
            _roomReservationRepository.Verify(m => m.DeleteMultipleAsync(It.IsAny<List<DataAccess.Entities.RoomReservation>>()), Times.Never);
            _roomReservationRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.Never);
            _roomReservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.RoomReservation>()), Times.AtLeastOnce);
            _reservationHistoryRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.ReservationHistory>()), Times.Never);
            _messagingEngine.Verify(m => m.PublishEventMessageAsync("room-events", (int)RoomEventType.Available, It.IsAny<RoomStatusEvent>()), Times.Never);
        }

    }
}
