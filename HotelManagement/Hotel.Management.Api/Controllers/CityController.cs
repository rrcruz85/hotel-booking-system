using Hotel.Management.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Management.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ILogger<CityController> _logger;

        public CityController(ICityService cityService, ILogger<CityController> logger)
        {
            _cityService = cityService;
            _logger = logger;
        }

        [HttpPost("Create")]
        public void CreateCity()
        {
          
        }
    }
}