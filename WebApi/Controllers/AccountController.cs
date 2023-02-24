using System.Security.Cryptography.X509Certificates;
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
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("Register")]
    // [Authorize(Roles = Roles.Student)]
[AllowAnonymous]
    public async Task<Response<IdentityResult>> Register([FromBody] RegistrDto model)
    {
        return await _accountService.Register(model);
    }

    [HttpPost("Login")]
    // [Authorize(Roles = Roles.Student)]

    [AllowAnonymous]

    public async Task<Response<TokenDto>> Login([FromBody] LoginDto model)
    {
        return await _accountService.Login(model);
    }

   
    [HttpGet("GetUsers")]

    public async Task<Response<List<UserDto>>> GetUsers()
    {
        return await _accountService.GetUsers();
    }
    
    [HttpGet("GetRole")]

    public async Task<Response<List<RoleDto>>> GetRols()
    {
        return await _accountService.GetRolesTask();
    }

    [HttpPost("AssighnRole")]

    public async Task<Response<AssignRoleDto>> AssignUserRole([FromBody] AssignRoleDto model)
    {
        return await _accountService.AssignUserRole(model);
    }
    
    [HttpDelete("DeleteRole")]
    public async Task<Response<AssignRoleDto>> DeleteRole([FromBody] AssignRoleDto model)
    {
        return await _accountService.DeleteRole(model);
    }
    
}