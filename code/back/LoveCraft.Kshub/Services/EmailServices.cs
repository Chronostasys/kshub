using LoveCraft.Kshub.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailAuthOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
       
        public EmailAuthOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string emails, string subject, string message)
        {
            var c = Environment.GetEnvironmentVariable("EmailSender");
            return Execute(c, subject, message, emails);
        }

        public Task Execute(string apiKey, string subject, string message, string emails)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("noreply@domain.com", "Bekenty Jean Baptiste"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };


                msg.AddTo(new EmailAddress(emails));
            

            Task response = client.SendEmailAsync(msg);
            return response;
        }
    }
}