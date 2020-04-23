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

namespace LoveCraft.Kshub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        EmailSender<SampleEmail> emailSender;
        public EmailController(EmailSender<SampleEmail> emailSender)
        {
            this.emailSender = emailSender;
        }
        [HttpPost("send")]
        public async ValueTask SendEmailAsync()
        {
            var e = new SampleEmail()
            {
                ExpectSendTime = DateTime.UtcNow,
                Receivers = new List<string>(),
            };
            e.Receivers.Add("2016231075@qq.com");
            await emailSender.QueueEmailAsync(e);
        }
    }
}

