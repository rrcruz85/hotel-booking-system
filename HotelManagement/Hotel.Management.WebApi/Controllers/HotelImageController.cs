using Hotel.Management.Service.Interfaces;
using Hotel.Management.WebApi.Translator;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelImageController : ControllerBase
    {
        private readonly IHotelGalleryService _hotelGalleryService;

        public HotelImageController(IHotelGalleryService hotelGalleryService)
        {
            _hotelGalleryService = hotelGalleryService;
        }

        // GET api/<HotelImageController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var image = await _hotelGalleryService.GetHotelImageByAsync(id);
            return Ok(image);
        }

        // GET api/<HotelImageController>/hotel/1
        [HttpGet("hotel/{hotelId:int}")]
        public async Task<IActionResult> GetByHotelIdAsync(int hotelId)
        {
            var images = await _hotelGalleryService.GetHotelGalleryAsync(hotelId);
            return Ok(images);
        }

        // POST api/<HotelImageController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Models.Requests.CreateHotelImage request)
        {
            var imageId = await _hotelGalleryService.CreateHotelImageAsync(request.ToBusinessModel());
            return Ok($"Image {imageId} was successfully created");
        }

        // PUT api/<HotelImageController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Models.Requests.UpdateHotelImage request)
        {
            await _hotelGalleryService.UpdateHotelImageAsync(request.ToBusinessModel());
            return Ok($"Image was successfully updated");
        }

        // DELETE api/<HotelImageController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _hotelGalleryService.DeleteHotelImageAsync(id);
            return Ok("Image deleted successfully");
        }
    }
}
