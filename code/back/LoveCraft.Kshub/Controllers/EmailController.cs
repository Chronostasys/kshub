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
using AutoMapper;
using Microsoft.Extensions.Hosting;

namespace LoveCraft.Kshub.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly KshubService _kshubService;
        readonly IMapper _mapper;
        IHostEnvironment env;
        public EmailController(KshubService kshubService, IMapper mapper, IHostEnvironment env)
        {
            _kshubService = kshubService;
            _mapper = mapper;
            this.env = env;
        }

        EmailSender<SampleEmail> emailSender;
        public EmailController(EmailSender<SampleEmail> emailSender)
        {
            this.emailSender = emailSender;
        }
        [HttpPost("SendEmail")]
        public async ValueTask SendEmailAsync(string email)
        {
            var e = new SampleEmail()
            {
                ExpectSendTime = DateTime.UtcNow,
                Receivers = new List<string>(),
                Sender="sample@limfx.pro",
                Subject="test",
                RazorTemplate="Index.cshtml"
            };
            e.Receivers.Add(email);
            await emailSender.QueueEmailAsync(e);
        }
    }
}

