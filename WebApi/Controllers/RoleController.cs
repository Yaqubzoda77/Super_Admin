using Domain.Contain;
using Domain.DomainDto;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[Route("[controller]")]
[Authorize(Roles = Roles.Admin)]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService; 

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;   
    }

    [HttpGet("GetRoles")]
    [AllowAnonymous]

    public async Task<Response<List<RoleDto>>> GetRoles()
    {
        return await _roleService.GetRolesTask();
    }

    [HttpPost("AssignRole")]
    [AllowAnonymous]

    public async Task<Response<AssignRoleDto>> AssignUserRole([FromBody] AssignRoleDto model)
    {
        return await _roleService.AssignUserRole(model);
    }

    [AllowAnonymous]
    [HttpDelete("RemoveRole")]
    public async Task<Response<AssignRoleDto>> RemoveRole([FromBody] AssignRoleDto model)
    {
        return await _roleService.RemoveUserRole(model);
    }

}