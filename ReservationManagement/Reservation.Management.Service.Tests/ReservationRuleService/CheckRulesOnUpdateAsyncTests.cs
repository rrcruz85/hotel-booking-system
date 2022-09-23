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
    public class CheckRulesOnUpdateAsyncTests : ReservationRuleServiceTest
    {
        [Test]
        public async Task When_StartDateIsHigherThanEndDate_Then_ResultShouldBe_FailedValidation()
        {
            _reservationContext.StartDate = DateTime.Now.AddDays(2);
            _reservationContext.EndDate = DateTime.Now.AddDays(1);

            var result = await _sut.CheckRulesOnCreateAsync(_reservationContext);
            Assert.IsFalse(result.Ok);
        }

        [Test]
        public async Task When_DurationIsHigherThanThreeDays_Then_ResultShouldBe_FailedValidation()
        {
            _reservationContext.StartDate = DateTime.Now.AddDays(1);
            _reservationContext.EndDate = DateTime.Now.AddDays(5);

            var result = await _sut.CheckRulesOnCreateAsync(_reservationContext);
            Assert.IsFalse(result.Ok);
        }

        [Test]
        public async Task When_StartDateIsHigherThanThirtyDaysFromCurrentDate_Then_ResultShouldBe_FailedValidation()
        {
            _reservationContext.StartDate = DateTime.Now.AddDays(35);
            _reservationContext.EndDate = DateTime.Now.AddDays(38);

            var result = await _sut.CheckRulesOnCreateAsync(_reservationContext);
            Assert.IsFalse(result.Ok);
        }

        [Test]
        public async Task When_ThereAreNotAvailableRooms_Then_ResultShouldBe_FailedValidation()
        {
            _reservationContext.StartDate = DateTime.Now.AddDays(3);
            _reservationContext.EndDate = DateTime.Now.AddDays(6);

            var rooms = new List<DataAccess.Entities.Room>()
            {
                new DataAccess.Entities.Room()
            };

            var queryResultMock = rooms.AsQueryable().BuildMock();

            _roomRepository.Setup(m => m.WhereQueryable(It.IsAny<Expression<Func<DataAccess.Entities.Room, bool>>>())).Returns(queryResultMock);

            var result = await _sut.CheckRulesOnCreateAsync(_reservationContext);

            Assert.IsFalse(result.Ok);
        }

        [Test]
        public async Task When_ThereAreAvailableRooms_Then_ResultShouldBe_OkValidation()
        {
            _reservationContext.StartDate = DateTime.Now.AddDays(3);
            _reservationContext.EndDate = DateTime.Now.AddDays(6);

            var rooms = new List<DataAccess.Entities.Room>();

            var queryResultMock = rooms.AsQueryable().BuildMock();

            _roomRepository.Setup(m => m.WhereQueryable(It.IsAny<Expression<Func<DataAccess.Entities.Room, bool>>>())).Returns(queryResultMock);

            var result = await _sut.CheckRulesOnCreateAsync(_reservationContext);

            Assert.IsTrue(result.Ok);
        }
    }
}
