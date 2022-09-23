using Microsoft.AspNetCore.Mvc;
using Reservation.Management.Service.Interfaces;
using Reservation.Management.WebApi.Models.Requests;
using Reservation.Management.WebApi.Translator;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reservation.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        // POST api/<SearchController>
        [HttpPost]
        public async Task<IActionResult> SearchRooms([FromBody] SearchCriteria searchCriteria)
        {
            try
            {
                var rooms = await _searchService.Search(searchCriteria.ToBusinessModel());
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }         
    }
}
