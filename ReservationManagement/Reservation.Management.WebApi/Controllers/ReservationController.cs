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
        public async Task<IActionResult> GetAsync(int id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            return Ok(reservation);
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
