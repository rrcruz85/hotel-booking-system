using Hotel.Management.Service.Interfaces;
using Hotel.Management.WebApi.Models.Requests;
using Hotel.Management.WebApi.Translator;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET api/<RoomController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var hotel = await _roomService.GetRoomByAsync(id);
            return Ok(hotel);
        }

        // GET api/<RoomController>/hotel/{1}
        [HttpGet("hotel/{hotelId:int}")]
        public async Task<IActionResult> GetByHotelAsync(int hotelId)
        {
            var rooms = await _roomService.GetAllRoomsByHotelIdAsync(hotelId);
            return Ok(rooms);
        }

        // GET api/<RoomController>/hotel/{1}/number/{1}
        [HttpGet("hotel/{hotelId:int}/number/{number:int}")]
        public async Task<IActionResult> GetByHotelAndNumberAsync(int hotelId, int number)
        {
            var rooms = await _roomService.GetRoomByNumberAsync(hotelId, number);
            return Ok(rooms);
        }

        // GET api/<RoomController>/hotel/{1}/status/{1}
        [HttpGet("hotel/{hotelId:int}/status/{status:int}")]
        public async Task<IActionResult> GetByHotelAndStatuAsync(int hotelId, int status)
        {
            var rooms = await _roomService.GetRoomsByStatusAndHotelAsync(hotelId, status);
            return Ok(rooms);
        }

        // GET api/<RoomController>/hotel/{1}/type/{1}
        [HttpGet("hotel/{hotelId:int}/status/{type:int}")]
        public async Task<IActionResult> GetByHotelAndTypeAsync(int hotelId, int type)
        {
            var rooms = await _roomService.GetRoomsByTypeAndHotelAsync(hotelId, type);
            return Ok(rooms);
        }

        // POST api/<RoomController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateRoom request)
        {
            var hotelId = await _roomService.CreateRoomAsync(request.ToBusinessModel());
            return Ok($"Room {hotelId} was successfully created");
        }

        // PUT api/<RoomController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateRoom request)
        {
            await _roomService.UpdateRoomAsync(request.ToBusinessModel());
            return Ok($"Room was successfully updated");
        }

        // DELETE api/<RoomController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roomService.DeleteRoomAsync(id);
            return Ok($"Room was successfully deleted");
        }

        // GET api/<RoomController>/statuses
        [HttpGet("statuses")]
        public IActionResult GetStatuses()
        {
            var statuses = _roomService.GetRoomStatuses();
            return Ok(statuses);
        }

        // GET api/<RoomController>/types
        [HttpGet("types")]
        public IActionResult GetTypes()
        {
            var types = _roomService.GetRoomTypes();
            return Ok(types);
        }
    }
}
