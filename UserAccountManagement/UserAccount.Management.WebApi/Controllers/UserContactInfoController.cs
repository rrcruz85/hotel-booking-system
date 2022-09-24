using UserAccount.Management.WebApi.Models.Requests;
using UserAccount.Management.WebApi.Translator;
using UserAccount.Management.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UserAccount.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContactInfoController : ControllerBase
    {
        private readonly IUserContactInfoService _userService;
        
        public UserContactInfoController(IUserContactInfoService userService)
        {
            _userService = userService;
        }

        // GET api/<UserContactInfoController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        // POST api/<UserContactInfoController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserContactInfo user)
        {
            var userId = await _userService.CreateUserContactInfoAsync(user.ToNewModel());
            return Ok($"User contact info {userId} was successfully created");
        }

        // PUT api/<UserContactInfoController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UserContactInfo user)
        {
            await _userService.UpdateUserContactInfoAsync(user.ToModel());
            return Ok("User contact info was successfully updated");
        }

        // DELETE api/<UserContactInfoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteUserContactInfoAsync(id);
            return Ok("User contact info was successfully deleted");
        }
    }
}
