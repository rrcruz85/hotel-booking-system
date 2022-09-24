using UserAccount.Management.WebApi.Models.Requests;
using UserAccount.Management.WebApi.Translator;
using UserAccount.Management.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UserAccount.Management.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService _roleService;
        
        public RolePermissionController(IRolePermissionService roleService)
        {
            _roleService = roleService;
        }

        // GET api/<RolePermissionController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var role = await _roleService.GetRolePermissionByIdAsync(id);
            return Ok(role);
        }


        // GET api/<RolePermissionController>/role/5
        [HttpGet("role/{roleId:int}")]
        public async Task<IActionResult> GetByRoleAsync(int roleId)
        {
            var roles = await _roleService.GetPermissionsByRoleIdAsync(roleId);
            return Ok(roles);
        }

        // POST api/<RolePermissionController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RolePermission role)
        {
            var categoryId = await _roleService.CreateRolePermissionAsync(role.ToNewModel());
            return Ok($"Role Permission {categoryId} was successfully created");
        }

        // PUT api/<RolePermissionController>
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] RolePermission role)
        {
            await _roleService.UpdateRolePermissionAsync(role.ToModel());
            return Ok("Role permission was successfully updated");
        }

        // DELETE api/<RolePermissionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.DeleteRolePermissionAsync(id);
            return Ok("Role permission was successfully deleted");
        }
    }
}
