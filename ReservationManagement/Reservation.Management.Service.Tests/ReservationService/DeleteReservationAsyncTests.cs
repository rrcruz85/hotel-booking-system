using Hotel.Booking.Common.Constant;
using Moq;
using NUnit.Framework;
using System;

namespace Reservation.Management.Service.Tests.ReservationService
{
    [TestFixture]
    public class DeleteReservationAsyncTests : ReservationServiceTest
    {

        [Test]
        public void When_ReservationDoesNotExist_Then_Exception_IsExpected()
        {
            _reservationRepository.Setup(m => m.FirstOrDefaultAsync(p => p.Id == _reservationContext.ReservationId)).ReturnsAsync(value: null);

            Assert.ThrowsAsync<ArgumentException>(() => _sut.DeleteReservationAsync(_reservationContext.ReservationId, 1));
        }

        [Test]
        public void When_ValidationsFail_Then_Exception_IsExpected()
        {
            _reservationEntity.Status = (int)ReservationStatus.OnGoing;
            _reservationEntity.StartDate = DateTime.Now.AddDays(-1);
            _reservationRepository.Setup(m => m.FirstOrDefaultAsync(p => p.Id == _reservationContext.ReservationId)).ReturnsAsync(_reservationEntity);

            Assert.ThrowsAsync<ArgumentException>(() => _sut.DeleteReservationAsync(_reservationContext.ReservationId, 1));
        }
    }
}
