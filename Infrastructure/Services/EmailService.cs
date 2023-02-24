using System.Net;
using System.Net.Mail;
using Domain.DomainDto;
using Domain.Setting;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class MailService 
{
    private readonly MailSettings _mailSettings;
    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }
    public async Task<bool> SendEmailAsync(MailRequest mailRequest)
    {
        try
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password),
                EnableSsl = true,
            };
             
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_mailSettings.Mail),
                Subject = mailRequest.Subject,
                Body = $"<h1>{mailRequest.Body}</h1>",
                IsBodyHtml = true,
                To = { mailRequest.ToEmail }
            };
            await smtpClient.SendMailAsync(mailMessage);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return  false ;
        }
         }
}