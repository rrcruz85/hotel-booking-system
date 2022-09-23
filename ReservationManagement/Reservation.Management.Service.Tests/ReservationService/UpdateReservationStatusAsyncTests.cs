using Hotel.Booking.Common.Constant;
using Moq;
using NUnit.Framework;
using Reservation.Management.Model;
using System;
using System.Linq.Expressions;

namespace Reservation.Management.Service.Tests.ReservationService
{
    [TestFixture]
    public class UpdateReservationStatusAsyncTests : ReservationServiceTest
    {

        [Test]
        public void When_ReservationDoesNotExist_Then_Exception_IsExpected()
        {
            _reservationRepository.Setup(m => m.FirstOrDefaultAsync(p => p.Id == _reservationContext.ReservationId)).ReturnsAsync(value: null);
            Assert.ThrowsAsync<ArgumentException>(() => _sut.UpdateReservationStatusAsync(_reservationContext.ReservationId, (int)ReservationStatus.OnGoing, 1, ""));
        }

        [Test]
        public void When_ReservationIsCanceled_Then_Exception_IsExpected()
        {
            _reservationEntity.Status = (int)ReservationStatus.Canceled;
            _reservationRepository.Setup(m => m.FirstOrDefaultAsync(p => p.Id == _reservationContext.ReservationId)).ReturnsAsync(_reservationEntity);
            Assert.ThrowsAsync<ArgumentException>(() => _sut.UpdateReservationStatusAsync(_reservationContext.ReservationId, (int)ReservationStatus.OnGoing, 1, ""));
        }

        [Test]
        public void When_ReservationStatusHasNOtChanged_Then_Exception_IsExpected()
        {
            _reservationRepository.Setup(m => m.FirstOrDefaultAsync(p => p.Id == _reservationContext.ReservationId)).ReturnsAsync(_reservationEntity);
            Assert.ThrowsAsync<ArgumentException>(() => _sut.UpdateReservationStatusAsync(_reservationContext.ReservationId, (int)ReservationStatus.Booked, 1, ""));
        }

        
        [Test]
        public void When_ValidationsAreOk_AndStatusHasNotChanged_AndRoomsEither_Then_ReservationIsUpdated_HistoryRecordIsNotCreated_AndBookedRoomEventsAreNotFired()
        {
            var okValidation = new ReservationRuleValidationResponse()
            {
                Ok = true
            };
            _config.Setup(m => m["RoomTopicName"]).Returns("room-events");

            _reservationRepository.Setup(m => m.FirstOrDefaultAsync(It.IsAny<Expression<Func<DataAccess.Entities.Reservation, bool>>>())).ReturnsAsync(_reservationEntity);

            Assert.DoesNotThrowAsync(() => _sut.UpdateReservationStatusAsync(_reservationContext.ReservationId, (int)ReservationStatus.OnGoing, 1, ""));

            _reservationRepository.Verify(m => m.UpdateAsync(It.IsAny<DataAccess.Entities.Reservation>()), Times.Once);
            _reservationHistoryRepository.Verify(m => m.AddAsync(It.IsAny<DataAccess.Entities.ReservationHistory>()), Times.Once);
        }
    }
}
