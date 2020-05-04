using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using LoveCraft.Kshub.Services;
using LoveCraft.Kshub.Models;
using LimFx.Business.Services;

namespace LoveCraft.Kshub.Services
{

    public class EmailService
    {
        EmailSender<Email> emailSender;
        public EmailService(EmailSender<Email> emailSender)
        {
            this.emailSender = emailSender;
        }

        public async ValueTask SendEmailAsync(EmailProperty emailProperty)
        {
            var e = new Email()
            {
                ExpectSendTime = DateTime.UtcNow,
                Sender = "sample@limfx.pro",
                Receivers = emailProperty.Receivers,
                Subject = emailProperty.Subject,
                RazorTemplate = emailProperty.RazorTemplatePath
            };
            await emailSender.QueueEmailAsync(e);
        }
    }


}