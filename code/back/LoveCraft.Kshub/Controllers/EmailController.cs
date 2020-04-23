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

namespace LoveCraft.Kshub.Controllers
{
    [Route("api/[controller]")]
    public class EmailSenderController : Controller
    {
        private readonly IEmailSender _emailSender;
        public EmailSenderController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [HttpPost]
        public async ValueTask SendEmailAsync(string email)
        {
            await _emailSender.SendEmailAsync(email, "主题aaa", "单邮件测试");
        }
    }
}

