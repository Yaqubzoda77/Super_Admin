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

public class RoleService 
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public RoleService(IConfiguration configuration, UserManager<IdentityUser> userManager, DataContext context,
        IMapper mapper)
    {
        _mapper = mapper;
        _configuration = configuration;
        _userManager = userManager;
        _context = context;
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
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id.ToUpper() == assign.RoleId.ToUpper());
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToUpper() == assign.UserId.ToUpper());
            await _userManager.AddToRoleAsync(user, role.Name);
            return new Response<AssignRoleDto>(assign);
        }
        catch (Exception ex)
        {
            return new Response<AssignRoleDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
    }

    public async Task<Response<AssignRoleDto>> RemoveUserRole(AssignRoleDto assign)
    {
        try
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id.ToUpper() == assign.RoleId.ToUpper());
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToUpper() == assign.UserId.ToUpper());
            await _userManager.RemoveFromRoleAsync(user, role.Name);
            return new Response<AssignRoleDto>(assign);
        }
        catch (Exception ex)
        {
            return new Response<AssignRoleDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });

        }
    } 

}