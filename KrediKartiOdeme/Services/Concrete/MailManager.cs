using KrediKartiOdeme.Services.Abstract;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrediKartiOdeme.Services.Concrete
{
    public class MailManager : IMailService
    {
        private IConfiguration _configuration;

        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _configuration.GetSection("APIKeys:SendGridAPIKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("tsecreditcardproject@hotmail.com", "Stajyer Burak Doğan");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
