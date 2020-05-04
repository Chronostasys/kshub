﻿using System;
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
        EmailSender<SampleEmail> emailSender;
        public EmailService(EmailSender<SampleEmail> emailSender)
        {
            this.emailSender = emailSender;
        }

        public async ValueTask SendEmailAsync(string email)
        {
            var e = new SampleEmail()
            {
                ExpectSendTime = DateTime.UtcNow,
                Receivers = new List<string>(),
                Sender = "sample@limfx.pro",
                Subject = "test",
                RazorTemplate = "Index.cshtml"
            };
            e.Receivers.Add(email);
            await emailSender.QueueEmailAsync(e);
        }
    }


}