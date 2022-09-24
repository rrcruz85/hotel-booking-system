using UserAccount.Management.WebApi.Models.Requests;
using UserAccount.Management.WebApi.Translator;
using UserAccount.Management.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UserAccount.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userService;
        
        public UserProfileController(IUserProfileService userService)
        {
            _userService = userService;
        }

        // GET api/<UserProfileController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        // POST api/<UserProfileController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserProfile user)
        {
            var userId = await _userService.CreateUserProfileAsync(user.ToNewModel());
            return Ok($"User profile {userId} was successfully created");
        }

        // PUT api/<UserProfileController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UserProfile user)
        {
            await _userService.UpdateUserProfileAsync(user.ToModel());
            return Ok("User profile was successfully updated");
        }

        // DELETE api/<UserProfileController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUserProfileAsync(id);
            return Ok("User profile was successfully deleted");
        }
    }
}
