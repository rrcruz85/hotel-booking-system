using UserAccount.Management.WebApi.Models.Requests;
using UserAccount.Management.WebApi.Translator;
using UserAccount.Management.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UserAccount.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET api/<RoleController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            return Ok(role);
        }

        // POST api/<RoleController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Role role)
        {
            var categoryId = await _roleService.CreateRoleAsync(role.ToNewModel());
            return Ok($"Role {categoryId} was successfully created");
        }

        // PUT api/<RoleController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Role role)
        {
            await _roleService.UpdateRoleAsync(role.ToModel());
            return Ok("Role was successfully updated");
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.DeleteRoleAsync(id);
            return Ok("Role was successfully deleted");
        }
    }
}
