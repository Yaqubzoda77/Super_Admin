using AutoMapper;
using Domain.DomainDto;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.MapperProfiles;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
      
        CreateMap<IdentityUser, UserDto>();
        CreateMap<IdentityRole, RoleDto>();
        CreateMap<Translate, TranslateDto>().ReverseMap();
        CreateMap<Language, LanguageDto>().ReverseMap();
        CreateMap<Group, GroupDto>().ReverseMap();
        
    }
}