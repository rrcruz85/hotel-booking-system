using Hotel.Management.Service.Interfaces;
using Hotel.Management.WebApi.Models.Requests;
using Hotel.Management.WebApi.Translator;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelContactInfoController : ControllerBase
    {
        private readonly IHotelContactInfoService _hotelContactInfoService;

        public HotelContactInfoController(IHotelContactInfoService hotelContactInfoService)
        {
            _hotelContactInfoService = hotelContactInfoService;
        }

        // GET api/<HotelContactInfoController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var contactInfo = await _hotelContactInfoService.GetHotelContactInfoByAsync(id);
            return Ok(contactInfo);
        }

        // GET api/<HotelContactInfoController>/hotel/1
        [HttpGet("hotel/{hotelId:int}")]
        public async Task<IActionResult> GetByHotelIdAsync(int hotelId)
        {
            var contactInfos = await _hotelContactInfoService.GetHotelContactInfoByHotelAsync(hotelId);
            return Ok(contactInfos);
        }

        // GET api/<HotelContactInfoController>/hotel/1/type1
        [HttpGet("hotel/{hotelId:int}/type/{type:int}")]
        public async Task<IActionResult> GetByHotelIdAndTypeAsync(int hotelId, int type)
        {
            var contactInfos = await _hotelContactInfoService.GetHotelContactInfoByHotelAndTypeAsync(hotelId, type);
            return Ok(contactInfos);
        }

        // POST api/<HotelContactInfoController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateHotelContactInfo request)
        {
            var categoryId = await _hotelContactInfoService.CreateHotelContactInfoAsync(request.ToBusinessModel());
            return Ok($"Contact Info {categoryId} was successfully created");
        }

        // PUT api/<HotelContactInfoController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateHotelContactInfo request)
        {
            await _hotelContactInfoService.UpdateHotelContactInfoAsync(request.ToBusinessModel());
            return Ok($"Contact Info was successfully updated");
        }

        // DELETE api/<HotelContactInfoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _hotelContactInfoService.DeleteHotelContactInfoAsync(id);
            return Ok();
        }

        // GET api/<HotelContactInfoController>/types
        [HttpGet("types")]
        public IActionResult GetAllContactInfoTypes()
        {
            var contactInfoTypes = _hotelContactInfoService.GetHotelContactInfoTypes();
            return Ok(contactInfoTypes);
        }
    }
}
