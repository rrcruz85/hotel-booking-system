using Hotel.Management.Service.Interfaces;
using Hotel.Management.WebApi.Models.Requests;
using Hotel.Management.WebApi.Translator;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelFacilityController : ControllerBase
    {
        private readonly IHotelFacilityService _hotelFacilityService;

        public HotelFacilityController(IHotelFacilityService hotelFacilityService)
        {
            _hotelFacilityService = hotelFacilityService;
        }

        // GET api/<HotelFacilityController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var facility = await _hotelFacilityService.GetHotelFacilityByIdAsync(id);
            return Ok(facility);
        }

        // GET api/<HotelFacilityController>/hotel/1
        [HttpGet("hotel/{hotelId:int}")]
        public async Task<IActionResult> GetByHotelIdAsync(int hotelId)
        {
            var facilties = await _hotelFacilityService.GetHotelFacilitiesByHotelAsync(hotelId);
            return Ok(facilties);
        }

        // POST api/<HotelFacilityController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateHotelFacility request)
        {
            var facilityId = await _hotelFacilityService.CreateHotelFacilityAsync(request.ToBusinessModel());
            return Ok($"Facility {facilityId} was successfully created");
        }

        // PUT api/<HotelFacilityController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateHotelFacility request)
        {
            await _hotelFacilityService.UpdateHotelFacilityAsync(request.ToBusinessModel());
            return Ok($"Facility was successfully updated");
        }

        // DELETE api/<HotelFacilityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _hotelFacilityService.DeleteHotelFacilityAsync(id);
            return Ok("Facility deleted successfully");
        }

    }
}
