using AutoMapper;
using Domain.DomainDto;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class UserService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserService(IConfiguration configuration, UserManager<IdentityUser> userManager, DataContext context,
        IMapper mapper)
    {
        _mapper = mapper; 
        _configuration = configuration;
        _userManager = userManager;
        _context = context;
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

}