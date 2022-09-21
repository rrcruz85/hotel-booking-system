using Hotel.Management.Service.Interfaces;
using Hotel.Management.WebApi.Models.Requests;
using Hotel.Management.WebApi.Translator;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // GET api/<HotelController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(id);
            return Ok(hotel);
        }

        // GET api/<HotelController>/5/details
        [HttpGet("{id:int}/details")]
        public async Task<IActionResult> GetDetailsAsync(int id)
        {
            var hotel = await _hotelService.GetHotelDetailsByIdAsync(id);
            return Ok(hotel);
        }

        // GET api/<HotelController>/city/1
        [HttpGet("city/{cityId:int}")]
        public async Task<IActionResult> GetByCityIdAsync(int cityId)
        {
            var hotels = await _hotelService.GetHotelsByCityIdAsync(cityId);
            return Ok(hotels);
        }

        // POST api/<HotelController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateHotel request)
        {
            var hotelId = await _hotelService.CreateHotelAsync(request.ToBusinessModel());
            return Ok($"Hotel {hotelId} was successfully created");
        }

        // PUT api/<HotelController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateHotel request)
        {
            await _hotelService.UpdateHotelAsync(request.ToBusinessModel());
            return Ok($"Hotel was successfully updated");
        }

        // DELETE api/<HotelController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _hotelService.DeleteHotelAsync(id);
            return Ok($"Hotel was successfully deleted");
        }
    }
}
