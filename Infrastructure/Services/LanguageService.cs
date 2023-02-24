using AutoMapper;
using Domain.DomainDto;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;


namespace Infrastructure.Services
{
    public class LanguageService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LanguageService(IConfiguration configuration,UserManager<IdentityUser> userManager, DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
        }
        public async Task<Response<List<LanguageDto>>> GetLanguage()
        {
            try
            {
                var result = await _context.Languages.ToListAsync();
                var mapped = _mapper.Map<List<LanguageDto>>(result);
                return new Response<List<LanguageDto>>(mapped);
            }
            catch (Exception ex)
            {
                return new Response<List<LanguageDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
            }
        }
        public async Task<Response<LanguageDto>> AddLanguage(LanguageDto languageDto)
        {
            try
            {
                var lang = _mapper.Map<Language>(languageDto);
                await _context.Languages.AddAsync(lang);
                await _context.SaveChangesAsync();
                return new Response<LanguageDto>(languageDto);
            }
            catch (Exception e)
            {
                return new Response<LanguageDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
            }
        }
        public async Task<Response<LanguageDto>> UpdateLanguage(LanguageDto languageDto)
        {
            try
            {
                var existing = await _context.Languages.Where(x => x.Id == languageDto.Id).AsNoTracking().FirstOrDefaultAsync();

                if (existing == null) return new Response<LanguageDto>(HttpStatusCode.BadRequest, new List<string>() { "Language not Found" });

                var mapped = _mapper.Map<Language>(languageDto);
                _context.Languages.Update(mapped);
                await _context.SaveChangesAsync();
                return new Response<LanguageDto>(languageDto);
            }
            catch (Exception ex)
            {
                return new Response<LanguageDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });

            }
        }
        public async Task<Response<string>> DeleteLanguage(int id)
        {
            var existing = await _context.Languages.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Id Not Found" });

            _context.Languages.Remove(existing);
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
    }
}
