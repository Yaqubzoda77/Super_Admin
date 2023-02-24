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
    public class TranslateService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TranslateService(IConfiguration configuration, UserManager<IdentityUser> userManager, DataContext context,
            IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration; 
            _userManager = userManager;
            _context = context;
        }
        public async Task<Response<List<TranslateDto>>> GetTranslate()
        {
            try
            {
                var result = await _context.Translations.ToListAsync();
                var mapped = _mapper.Map<List<TranslateDto>>(result);
                return new Response<List<TranslateDto>>(mapped); 
            }
            catch (Exception ex)
            {
                return new Response<List<TranslateDto>>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
            }
        }
        public async Task<Response<TranslateDto>> AddTranslate(TranslateDto translateDto)
        {
            try
            {
                var roles = _mapper.Map<Translate>(translateDto);
                await _context.Translations.AddAsync(roles);
                await _context.SaveChangesAsync();
                return new Response<TranslateDto>(translateDto);
            }
            catch (Exception e)
            {
                return new Response<TranslateDto>(HttpStatusCode.InternalServerError, new List<string>() { e.Message });
            }
        }
        public async Task<Response<TranslateDto>> UpdateTranslate(TranslateDto traslateDto)
        {
            try
            {
                var existing = await _context.Translations.Where(x => x.Id == traslateDto.Id).AsNoTracking().FirstOrDefaultAsync();

                if (existing == null) return new Response<TranslateDto>(HttpStatusCode.BadRequest, new List<string>() { " Translate not Found" });

                var mapped = _mapper.Map<Translate>(traslateDto);
                _context.Translations.Update(mapped);
                await _context.SaveChangesAsync();
                return new Response<TranslateDto>(traslateDto);
            }
            catch (Exception ex)
            {
                return new Response<TranslateDto>(HttpStatusCode.InternalServerError, new List<string> { ex.Message });

            }
        }
        public async Task<Response<string>> DeleteTranslate(int id) 
        {
            var existing = await _context.Translations.FindAsync(id);
            if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Id Not Found" });

            _context.Translations.Remove(existing);
            await _context.SaveChangesAsync();
            return new Response<string>("Deleted successfully");
        }
    }
}
