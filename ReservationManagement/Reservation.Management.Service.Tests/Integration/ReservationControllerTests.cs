using Hotel.Booking.Common.Contract.Messaging;
using Hotel.Booking.Common.Contract.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Reservation.Management.DataAccess;
using Reservation.Management.WebApi.Models.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Reservation.Management.WebApi.Models.Requests.CreateReservation;

namespace Reservation.Management.Service.Tests.Integration
{
    [TestFixture]
    public class ReservationControllerTests
    {
        private DateTime StartDate;
        private DateTime EndDate;

        [SetUp]
        public void SetUp()
        {
            StartDate = DateTime.Now.AddDays(3);
            EndDate = DateTime.Now.AddDays(6);
        }

        [Test]
        public async Task When_ThereAreAvailableRooms_Then_CreateReservationRequestCall_ShouldReturn_OkRequestResponse_With_ReservationId()
        {
            var httpClient = GetTestServer(false).CreateClient();

            var _requestObj = new CreateReservation
            {
                StartDate = StartDate,
                EndDate = EndDate,
                HotelId = 1,
                Rooms = new List<CreateRoomReservation>()
                { 
                    new CreateRoomReservation
                    {
                        RoomId = 1,
                        Price = 100
                    }
                },
                UserId = 1
            };

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/api/reservation/")
            {
                Content = new StringContent(JsonConvert.SerializeObject(_requestObj), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(httpRequest);

            Assert.IsTrue(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.That(responseContent == "Reservation 1 was successfully created");
        }


        [Test]
        public async Task When_ThereAreNotAvailableRooms_Then_CreateReservationRequestCall_ShouldReturn_BadRequestResponse()
        {
            
            var httpClient = GetTestServer(true).CreateClient();

            var _requestObj = new CreateReservation
            {
                StartDate = StartDate,
                EndDate = EndDate,
                HotelId = 1,
                Rooms = new List<CreateRoomReservation>()
                {
                    new CreateRoomReservation
                    {
                        RoomId = 1,
                        Price = 100
                    }
                },
                UserId = 1
            };

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/api/reservation/")
            {
                Content = new StringContent(JsonConvert.SerializeObject(_requestObj), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(httpRequest);

            Assert.IsFalse(response.IsSuccessStatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();

            Assert.That(responseContent == "Rooms are not available in the selectect range of dates");
        }

        private TestServer GetTestServer(bool withReservations)
        {
            var webServer = new WebApplicationFactory<Program>().WithWebHostBuilder(config =>
            {
                config.ConfigureServices(services =>
                {
                    var dbDescriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<ReservationManagementContext>));
                    if (dbDescriptor != null)
                    {
                        services.Remove(dbDescriptor);
                    }

                    var blobDescriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(IBlobStorageService));
                    if (blobDescriptor != null)
                    {
                        services.Remove(blobDescriptor);
                    }

                    var messagingDescriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(IMessagingEngine));
                    if (messagingDescriptor != null)
                    {
                        services.Remove(messagingDescriptor);
                    }

                    var messagingEngine = new Mock<IMessagingEngine>();
                    messagingEngine.Setup(m => m.PublishEventMessageAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<object>()));
                    services.AddScoped(services => messagingEngine.Object);

                    var blobService = new Mock<IBlobStorageService>();
                    blobService.Setup(m => m.UploadStreamAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()));
                    blobService.Setup(m => m.UploadTextAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
                    blobService.Setup(m => m.GetBlobContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()));
                    blobService.Setup(m => m.GetBlobNames(It.IsAny<string>(), null));
                    services.AddScoped(services => blobService.Object);

                    services.AddDbContext<ReservationManagementContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ReservationManagementContext>();

                    db.Database.EnsureCreated();

                    if (withReservations)
                        DbUtility.BookAllRoomsDbForTests(db, StartDate, EndDate);
                });
            });

            return webServer.Server;
        }
    }
}
