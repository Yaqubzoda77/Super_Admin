
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Domain.DomainDto;
using Infrastructure.Services;

namespace _101SendEmailNotificationDoNetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {

        private readonly MailService mailService;
        public EmailController(MailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            
            await mailService.SendEmailAsync(request);
                return Ok();

        }


    }
}