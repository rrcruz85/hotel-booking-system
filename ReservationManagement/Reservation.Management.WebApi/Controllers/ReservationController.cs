using Microsoft.AspNetCore.Mvc;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.WebApi.Models.Requests;
using Reservation.Management.WebApi.Translator;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reservation.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService= reservationService;
        }

        // GET api/<ReservationController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReservationAsync(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            return Ok(reservation);
        }

        // GET api/<ReservationController>/5/details
        [HttpGet("{id:int}/details")]
        public async Task<IActionResult> GetReservationDetailsAsync(int id)
        {
            var reservation = await _reservationService.GetReservationDetailsByIdAsync(id);
            return Ok(reservation);
        }

        // GET api/<ReservationController>/hotel/1
        [HttpGet("hotel/{hotelId:int}")]
        public async Task<IActionResult> GetReservationsByHotelsAsync(int hotelId)
        {
            var reservations = await _reservationService.GetReservationsByHotelIdAsync(hotelId);
            return Ok(reservations);
        }

        // GET api/<ReservationController>/user/1
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetReservationsByUsersAsync(int userId)
        {
            var reservations = await _reservationService.GetReservationsByUserIdAsync(userId);
            return Ok(reservations);
        }

        // GET api/<ReservationController>/hotel/1/2022-09-01/2022-09-15
        [HttpGet("hotel/{hotelId:int}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetReservationsByHotelAndDatesAsync(int hotelId, DateTime startDate, DateTime endDate)
        {
            var reservations = await _reservationService.GetReservationsByHotelIdAndDatesAsync(hotelId, startDate, endDate);
            return Ok(reservations);
        }

        // POST api/<ReservationController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateReservation request)
        {
            try
            {
                var reservationId = await _reservationService.CreateReservationAsync(request.ToBusinessModel());
                return Ok($"Reservation {reservationId} was successfully created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ReservationController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateReservation request)
        {
            try
            {
                await _reservationService.UpdateReservationAsync(request.ToBusinessModel());
                return Ok($"Reservation was successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ReservationController>/5
        [HttpDelete("{id}/{userId}")]
        public async Task<IActionResult> Delete(int id, int userId)
        {
            try
            {
                await _reservationService.DeleteReservationAsync(id, userId);
                return Ok("Reservation deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }      

    }
}
