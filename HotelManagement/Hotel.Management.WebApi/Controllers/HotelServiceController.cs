using Hotel.Management.Service.Interfaces;
using Hotel.Management.WebApi.Models.Requests;
using Hotel.Management.WebApi.Translator;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelServiceController : ControllerBase
    {
        private readonly IServicesForHotelService _servicesForHotelService;

        public HotelServiceController(IServicesForHotelService servicesForHotelService)
        {
            _servicesForHotelService = servicesForHotelService;
        }

        // GET api/<HotelServiceController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var service = await _servicesForHotelService.GetHotelServiceByAsync(id);
            return Ok(service);
        }

        // GET api/<HotelServiceController>/hotel/1
        [HttpGet("hotel/{hotelId:int}")]
        public async Task<IActionResult> GetByHotelIdAsync(int hotelId)
        {
            var services = await _servicesForHotelService.GetHotelServiceByHotelAsync(hotelId);
            return Ok(services);
        }

        // POST api/<HotelServiceController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateHotelService request)
        {
            var serviceId = await _servicesForHotelService.CreateHotelServiceAsync(request.ToBusinessModel());
            return Ok($"Service {serviceId} was successfully created");
        }

        // PUT api/<HotelServiceController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateHotelService request)
        {
            await _servicesForHotelService.UpdateHotelServiceAsync(request.ToBusinessModel());
            return Ok($"Service was successfully updated");
        }

        // DELETE api/<HotelServiceController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _servicesForHotelService.DeleteHotelServiceAsync(id);
            return Ok("Service deleted successfully");
        }
    }
}
