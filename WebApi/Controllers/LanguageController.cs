using Domain.Contain;
using Domain.DomainDto;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = Roles.SuperAdmin)]
    public class LanguageController : ControllerBase 
    {
        private readonly LanguageService _languageService; 

        public LanguageController(LanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet("Get")]
        [AllowAnonymous]
        public async Task<Response<List<LanguageDto>>> Get()
        {
            return await _languageService.GetLanguage();
        }

        [HttpPost("Add")]
        [AllowAnonymous]
        public async Task<Response<LanguageDto>> Add(LanguageDto languageDto)
        {
            if (ModelState.IsValid)
            {
                return await _languageService.AddLanguage(languageDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new Response<LanguageDto>(HttpStatusCode.BadRequest, errors);
            }
        }
        [HttpPut("Update")]
        [AllowAnonymous]
        public async Task<Response<LanguageDto>> Put([FromBody] LanguageDto languageDto)
        {

            return await _languageService.UpdateLanguage(languageDto);
        }
        [HttpDelete("Delete")]
        [AllowAnonymous]
        public async Task<Response<string>> Delete(int id)
        {
            return await _languageService.DeleteLanguage(id);
        }
    }
}
