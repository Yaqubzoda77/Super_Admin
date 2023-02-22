using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain.DomainDto;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;
    
    public class AccountService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AccountService(IConfiguration configuration, UserManager<IdentityUser> userManager, DataContext context,
        IMapper mapper)
    {
        _mapper = mapper;

        _configuration = configuration;
        _userManager = userManager;
        _context = context;
    }

    public async Task<Response<IdentityResult>> Register(RegistrDto registerDto)
    {

        var user = new IdentityUser()
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        return new Response<IdentityResult>(result);
    }

    
    public async Task<Response<List<UserDto>>> GetUsers()
    {
        var users = await _context.Users.Select(x => new UserDto()
            {
                Id = x.Id,
                Username = x.UserName,
                PhoneNumber = x.PhoneNumber,
                Roles = (from u in _context.UserRoles
                    join r in _context.Roles on u.RoleId equals r.Id
                    where x.Id == u.UserId
                    select new RoleDto()
                    {
                        Id = r.Id,
                        Name = r.Name
                    }).ToList()
            }
        ).ToListAsync();
    return new Response<List<UserDto>>(_mapper.Map<List<UserDto>>(users));
    }

    
    
    public async Task<Response<TokenDto>> Login(LoginDto model)
    {
        var existing = await _userManager.FindByEmailAsync(model.Email);
        if (existing == null)
            return new Response<TokenDto>(HttpStatusCode.BadRequest,
                new List<string>() { "Incorrect passsword or login" });

        
        var check = await _userManager.CheckPasswordAsync(existing, model.Password);
        if (check == true)
        {
            return new Response<TokenDto>(await GenerateJWTToken(existing));
        }
        else
        {
            return new Response<TokenDto>(HttpStatusCode.BadRequest, new List<string>());
        }
    }
    
    public async Task<Response<List<RoleDto>>> GetRolesTask()
    {
        var roles = await _context.Roles.ToListAsync();
        return new Response<List<RoleDto>>(_mapper.Map<List<RoleDto>>(roles));
    }

   


    public async Task<Response<AssignRoleDto>> AssignUserRole(AssignRoleDto assign)
    {
        try
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x =>x.Id.ToUpper() == assign.RoleId.ToUpper());
            var user = await _context.Users.FirstOrDefaultAsync(x =>x.Id.ToUpper() == assign.UserId.ToUpper());
            await _userManager.AddToRoleAsync(user, role.Name);
            return new Response<AssignRoleDto>(assign);
        }
        catch (Exception ex)
        {
            return new Response<AssignRoleDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});

        }
    }

   

    public async Task<Response<AssignRoleDto>> DeleteRole(AssignRoleDto assign)
    {
        try
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x =>x.Id.ToUpper() == assign.RoleId.ToUpper());
            var user = await _context.Users.FirstOrDefaultAsync(x =>x.Id.ToUpper() == assign.UserId.ToUpper());
            await _userManager.RemoveFromRoleAsync(user, role.Name);
            return new Response<AssignRoleDto>(assign);
        }
        catch (Exception ex)
        {
            return new Response<AssignRoleDto>(HttpStatusCode.InternalServerError,new List<string>(){ex.Message});

        }
    }
    

    private async Task<TokenDto> GenerateJWTToken(IdentityUser user)
    {
        return await Task.Run(() =>
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin")
                
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return new TokenDto(tokenString);
        });
        

    }
}