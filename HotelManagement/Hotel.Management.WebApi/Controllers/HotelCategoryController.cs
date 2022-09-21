using Hotel.Management.WebApi.Models.Requests;
using Hotel.Management.WebApi.Translator;
using Hotel.Management.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelCategoryController : ControllerBase
    {
        private readonly IHotelCategoryService _hotelCategoryService;
        private readonly IHotelCategoryRelationService _hotelCategoryRelationService;

        public HotelCategoryController(
            IHotelCategoryService hotelCategoryService, 
            IHotelCategoryRelationService hotelCategoryRelationService)
        {
            _hotelCategoryService = hotelCategoryService;
            _hotelCategoryRelationService = hotelCategoryRelationService;
        }

        // GET api/<HotelCategoryController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var category = await _hotelCategoryService.GetHotelCategoryByAsync(id);
            return Ok(category);
        }

        // GET api/<HotelCategoryController>/hotel/1
        [HttpGet("hotel/{hotelId:int}")]
        public async Task<IActionResult> GetByHotelIdAsync(int hotelId)
        {
            var categories = await _hotelCategoryService.GetHotelCategoriesByHotelAsync(hotelId);
            return Ok(categories);
        }

        // POST api/<HotelCategoryController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateHotelCategory request)
        {
            var categoryId = await _hotelCategoryService.CreateHotelCategoryAsync(request.ToBusinessModel());
            return Ok($"Category {categoryId} was successfully created");
        }

        // PUT api/<HotelCategoryController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateHotelCategory request)
        {
            await _hotelCategoryService.UpdateHotelCategoryAsync(request.ToBusinessModel());
            return Ok($"Category was successfully updated");
        }

        // DELETE api/<HotelCategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _hotelCategoryService.DeleteHotelCategoryAsync(id);
            return Ok();
        }

        // POST api/<HotelCategoryController>/relation
        [HttpPost("relation")]
        public async Task<IActionResult> PostHotelCategoryRelationAsync([FromBody] HotelCategoryRelation request)
        {
            var relationId = await _hotelCategoryRelationService.CreateHotelCategoryRelationAsync(request.HotelId, request.CategoryId);
            return Ok($"Relation {relationId} was successfully created");
        }

        // DELETE api/<HotelCategoryController>/relation
        [HttpDelete("relation")]
        public async Task<IActionResult> DeleteHotelCategoryRelationAsync([FromBody] HotelCategoryRelation request)
        {
            await _hotelCategoryRelationService.DeleteHotelCategoryRelationAsync(request.HotelId, request.CategoryId);
            return Ok($"Relation was successfully deleted");
        }

    }
}
