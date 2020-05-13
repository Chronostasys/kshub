using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoveCraft.Kshub.Dto;
using LoveCraft.Kshub.Services;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using LoveCraft.Kshub.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Identity;
using LimFx.Business.Services;
using Microsoft.Extensions.Configuration;
using LoveCraft.Kshub.Exceptions;
namespace LoveCraft.Kshub.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class KshubUserController : Controller
    {
        private readonly SecretRecord _secretRecord;
        private readonly KshubService _kshubService;
        readonly IMapper _mapper;
        IHostEnvironment env;
        public KshubUserController(KshubService kshubService, IMapper mapper, IHostEnvironment env,SecretRecord secretRecord)
        {
            _secretRecord = secretRecord;
            _kshubService = kshubService;
            _mapper = mapper;
            this.env = env;
        }

        [HttpGet]
        public async ValueTask<KshubUserDetailDto> GetUsersAsync(string id)
        {
            var user = await _kshubService.KshubUserServices.FindUserAsync(id);
            return _mapper.Map<KshubUserDetailDto>(user);
        }

        [HttpPost]
        [Route("Register")]
        public async ValueTask<KshubUserDetailDto> Register(AddUserDto addUserDto)
        {
            var user = new KshubUser
            {
                Id = Guid.NewGuid(),
                Name = addUserDto.Name,
                SchoolName = addUserDto.SchoolName,
                UserId = addUserDto.UserId,
                Introduction = addUserDto.Introduction,
                Email = addUserDto.Email,
                PassWordHash = addUserDto.Password,
                Roles = new List<string> { "User" },
                IsEmailConfirmed = false
            };

            await _kshubService.KshubUserServices.AddUserAsync(user);
            return _mapper.Map<KshubUserDetailDto>(user);
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
        public async ValueTask<KshubUserDetailDto> LogIn(LogInDto logInDto,bool rememberMe)
        {
            
            if (!User.Identity.IsAuthenticated)
            {
                return _mapper.Map<KshubUserDetailDto>(await _kshubService.KshubUserServices.SignInAsAnonymous(HttpContext));
            }
            //已经认证过了，dto传进来的UserId是空的，
            else if (string.IsNullOrWhiteSpace(logInDto.UserId))
            {
                try
                {
                    return await _kshubService.KshubUserServices.FindFirstAsync(u =>Guid.Parse(User.Identity.Name) == u.Id,
                        u => _mapper.Map<KshubUserDetailDto>(u));
                }
                catch (Exception)
                {
                    throw new _500Exception();
                }
            }
            else
            {
                var user = await _kshubService.KshubUserServices.FindUserAsync(logInDto.UserId);

                //==========================
                //测试把IsEamcilConfirmed设为false检测发邮件

                //======================
                if (user == null)
                {
                    throw new _400Exception("Username or Password is wrong.");
                }
                else if (!user.IsEmailConfirmed)
                {

                    var emailProperty = new EmailProperty()
                    {
                        RazorTemplatePath = "\\EmailTemplate\\EmailConfirm.cshtml",
                        Subject = "Confirm Kshub Account's Email",
                        Receivers = new List<string> { user.Email },
                        Url= Url.Content($"{Request.Scheme}://{Request.Host.Value}/api/KshubUser/ValidateEmail/{user.Id}")
                };
                    await _kshubService.EmailService.SendEmailAsync(emailProperty);
                    throw new _401Exception("Please verify your email!");
                }
                else
                {
                    user.PassWordHash = logInDto.Password;
                    await _kshubService.KshubUserServices.LogInAsync(user, HttpContext, rememberMe);
                }
                return _mapper.Map<KshubUserDetailDto>(user);
            }
        }

        [HttpPost]
        [Route("Signout")]
        public async ValueTask<KshubUserDetailDto> SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return _mapper.Map<KshubUserDetailDto>(await _kshubService.KshubUserServices.SignInAsAnonymous(HttpContext));
        }



    }
}
