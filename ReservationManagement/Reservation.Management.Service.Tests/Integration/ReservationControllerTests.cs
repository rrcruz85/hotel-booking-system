using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using Reservation.Management.DataAccess;
using Reservation.Management.WebApi.Models.Requests;
using System;
using System.Collections.Generic;
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
        private TestServer _testServer;

        [SetUp]
        public void SetUp()
        {
            
            var webServer = new WebApplicationFactory<Program>().WithWebHostBuilder(config =>
            {
                config.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<ReservationManagementContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }
                                       
                    services.AddDbContext<ReservationManagementContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ReservationManagementContext>();
                    
                    db.Database.EnsureCreated();                   
                });
            });

            _testServer = webServer.Server;
        }

        [Test]
        public async Task When_ThereAreAvailableRooms_Then_CreateReservationRequestCall_ShouldReturn_OkRequestResponse_With_ReservationId()
        {
            var httpClient = _testServer.CreateClient();

            var _requestObj = new CreateReservation
            {
                StartDate = DateTime.Now.AddDays(3),
                EndDate = DateTime.Now.AddDays(6),
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
    }
}
