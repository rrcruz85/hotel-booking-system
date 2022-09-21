using Hotel.Management.WebApi.Models.Requests;
using Hotel.Management.WebApi.Translator;
using Hotel.Management.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        // GET api/<CityController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var city = await _cityService.GetCityByAsync(id);
            return Ok(city);
        }

        // GET api/<CityController>
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var cities = await _cityService.GetCitiesByNameAsync(name);
            return Ok(cities);
        }

        // GET api/<CityController>
        [HttpGet("bycountry/{country}")]
        public async Task<IActionResult> GetByCountryAsync(string country)
        {
            var cities = await _cityService.GetCitiesByCountryAsync(country);
            return Ok(cities);
        }

        // GET api/<CityController>
        [HttpGet("bystate/{state}/{country}")]
        public async Task<IActionResult> GetByStateCountryAsync(string state, string country)
        {
            var cities = await _cityService.GetCitiesByStateCountryAsync(state, country);
            return Ok(cities);
        }

        // POST api/<CityController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateCity request)
        {
            var categoryId = await _cityService.CreateCityAsync(request.ToBusinessModel());
            return Ok($"City {categoryId} was successfully created");
        }

        // PUT api/<CityController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateCity request)
        {
            await _cityService.UpdateCityAsync(request.ToBusinessModel());
            return Ok("City was successfully updated");
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cityService.DeleteCityAsync(id);
            return Ok("City was successfully deleted");
        }
    }
}
