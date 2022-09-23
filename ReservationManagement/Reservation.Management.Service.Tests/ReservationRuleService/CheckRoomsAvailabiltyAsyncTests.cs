using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Reservation.Management.Service.Tests.ReservationRuleService
{
    [TestFixture]
    public class CheckRoomsAvailabiltyAsyncTests: ReservationRuleServiceTest
    {
        [Test]
        public async Task When_StartDateIsPriorToCurrentDay_Then_ResultShouldBe_False()
        {
            _reservationContext.StartDate = DateTime.Now.AddDays(-1);
            var result = await _sut.CheckRoomsAvailabiltyAsync(_reservationContext);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task When_StartDateIsPriorToCurrentDay_AndEndDateIsAfterCurrentDay_Then_ResultShouldBe_False()
        {
            _reservationContext.StartDate = DateTime.Now.AddDays(-1);
            _reservationContext.EndDate = DateTime.Now.AddDays(1);
            var result = await _sut.CheckRoomsAvailabiltyAsync(_reservationContext);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task When_ThereAreNotAvailableRooms_Then_ResultShouldBe_False()
        {
            _reservationContext.StartDate = DateTime.Now.AddDays(1);
            _reservationContext.EndDate = DateTime.Now.AddDays(2);

            var rooms = new List<DataAccess.Entities.Room>()
            {
                new DataAccess.Entities.Room()
            };

            var queryResultMock = rooms.AsQueryable().BuildMock();

            _roomRepository.Setup(m => m.WhereQueryable(It.IsAny<Expression<Func<DataAccess.Entities.Room, bool>>>())).Returns(queryResultMock);

            var result = await _sut.CheckRoomsAvailabiltyAsync(_reservationContext);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task When_ThereAreAvailableRooms_Then_ResultShouldBe_True()
        {
            _reservationContext.StartDate = DateTime.Now.AddDays(1);
            _reservationContext.EndDate = DateTime.Now.AddDays(2);

            var rooms = new List<DataAccess.Entities.Room>();

            var queryResultMock = rooms.AsQueryable().BuildMock();

            _roomRepository.Setup(m => m.WhereQueryable(It.IsAny<Expression<Func<DataAccess.Entities.Room, bool>>>())).Returns(queryResultMock);

            var result = await _sut.CheckRoomsAvailabiltyAsync(_reservationContext);

            Assert.IsTrue(result);
        }
    }
}
