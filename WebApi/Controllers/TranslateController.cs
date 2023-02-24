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
    public class TranslateController : ControllerBase
    {
        private readonly TranslateService _translateService;

        public TranslateController(TranslateService translateService)
        {
            _translateService = translateService;
        }

        [HttpGet("Get")]
        [AllowAnonymous]
        public async Task<Response<List<TranslateDto>>> GetTranslate() 
        {
            return await _translateService.GetTranslate();
        }

        [HttpPost("Add")]
        [AllowAnonymous]
        public async Task<Response<TranslateDto>> Add(TranslateDto traslateDto)
        {
            if (ModelState.IsValid)
            {
                return await _translateService.AddTranslate(traslateDto);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new Response<TranslateDto>(HttpStatusCode.BadRequest, errors);
            }
        }
        [HttpPut("Update")]
        [AllowAnonymous]
        public async Task<Response<TranslateDto>> Update([FromBody] TranslateDto traslateDto)
        {

            return await _translateService.UpdateTranslate(traslateDto);  
        }
        [HttpDelete("Delete")]
        [AllowAnonymous]
        public async Task<Response<string>> Delete(int id)
        {
            return await _translateService.DeleteTranslate(id);
        }
    }
}
