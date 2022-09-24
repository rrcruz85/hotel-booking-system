using UserAccount.Management.WebApi.Models.Requests;
using UserAccount.Management.WebApi.Translator;
using UserAccount.Management.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UserAccount.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/<UserController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] User user)
        {
            var userId = await _userService.CreateUserAsync(user.ToNewModel());
            return Ok($"User {userId} was successfully created");
        }

        // PUT api/<UserController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] User user)
        {
            await _userService.UpdateUserAsync(user.ToModel());
            return Ok("User was successfully updated");
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok("User was successfully deleted");
        }
    }
}
