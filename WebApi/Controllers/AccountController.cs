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
    
}