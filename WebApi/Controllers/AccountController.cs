using Domain.Contain;
using Domain.DomainDto;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[Route("[controller]")]
[Authorize(Roles = Roles.SuperAdmin)]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("Register")]
   [AllowAnonymous]
    public async Task<Response<IdentityResult>> Register([FromBody] RegistrDto model)
    {
        return await _accountService.Register(model);
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<Response<TokenDto>> Login([FromBody] LoginDto model)
    {
        return await _accountService.Login(model);
    }
 
    [HttpGet("GetUsers")]
    [AllowAnonymous]
    public async Task<Response<List<UserDto>>> GetUsers()
    {
        return await _accountService.GetUsers();
    }
    
    [HttpGet("GetRoles")]
    [AllowAnonymous]

    public async Task<Response<List<RoleDto>>> GetRoles()
    {
        return await _accountService.GetRolesTask(); 
    }

    [HttpPost("AssignRole")]
    [AllowAnonymous]
    public async Task<Response<AssignRoleDto>> AssignUserRole([FromBody] AssignRoleDto model)
    {
        return await _accountService.AssignUserRole(model);
    }
    [AllowAnonymous]
    [HttpDelete("RemoveRole")]   
    public async Task<Response<AssignRoleDto>> RemoveRole([FromBody] AssignRoleDto model)
    {
        return await _accountService.RemoveUserRole(model); 
    }
    
}