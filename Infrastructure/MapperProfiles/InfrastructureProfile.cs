using AutoMapper;
using Domain.DomainDto;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.MapperProfiles;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
      
        CreateMap<IdentityUser, UserDto>();
        CreateMap<IdentityRole, RoleDto>();

    }
}