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
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;  
    }

    [HttpGet("GetUsers")]
    [AllowAnonymous]
    public async Task<Response<List<UserDto>>> GetUsers()
    {
        return await _userService.GetUsers();
    }

}