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
using DocumentFormat.OpenXml.Office2010.Excel;
using MongoDB.Driver;
using System.Security.Authentication;
namespace LoveCraft.Kshub.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly SecretRecord _secretRecord;
        private readonly KshubService _kshubService;
        readonly IMapper _mapper;
        IHostEnvironment env;
        public UserController(KshubService kshubService, IMapper mapper, IHostEnvironment env,SecretRecord secretRecord)
        {
            _secretRecord = secretRecord;
            _kshubService = kshubService;
            _mapper = mapper;
            this.env = env;
        }

        [HttpGet]
        public async ValueTask<UserDetailDto> GetUsersAsync(string id)
        {
            var user = await _kshubService.KshubUserServices.FindUserAsync(id);
            return _mapper.Map<UserDetailDto>(user);
        }

        [HttpPost]
        [Route("Register")]
        public async ValueTask Register(AddUserDto addUserDto)
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
                IsEmailConfirmed = true,
                //应该提供默认头像的url，之后加上
                AvatarUrl=null
            };
            try
            {
                await _kshubService.KshubUserServices.GetUserByEmailAsync(user.Email);
                throw new _401Exception("This email has register already,if you forget your password,please reset your password");

            }
            //没找到就去注册
            catch (Exception)
            {
                await _kshubService.KshubUserServices.AddUserAsync(user);
            }

        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
        public async ValueTask<UserDetailDto> LogIn(LogInDto logInDto,bool rememberMe)
        {
            
            if (!User.Identity.IsAuthenticated)
            {
                return _mapper.Map<UserDetailDto>(await _kshubService.KshubUserServices.SignInAsAnonymous(HttpContext));
            }
            //已经认证过了，dto传进来的UserId是空的，
            else if (string.IsNullOrWhiteSpace(logInDto.UserId))
            {
                try
                {
                    return await _kshubService.KshubUserServices.FindFirstAsync(u =>Guid.Parse(User.Identity.Name) == u.Id,
                        u => _mapper.Map<UserDetailDto>(u));
                }
                catch (Exception)
                {
                    throw new _500Exception();
                }
            }
            else
            {
                var user = await _kshubService.KshubUserServices.FindUserAsync(logInDto.UserId);


                if (user == null)
                {
                    throw new _400Exception("Username or Password is wrong.");
                }
                else
                {
                    user.PassWordHash = logInDto.Password;
                    await _kshubService.KshubUserServices.LogInAsync(user, HttpContext, rememberMe);
                }
                return _mapper.Map<UserDetailDto>(user);
            }
        }

        [HttpPost]
      
        [Route("ValidateEmail")]
        public async ValueTask<UserDetailDto> ValidateEmailAsync(Guid guid)
        {
            _kshubService.tokens.TryRemove(guid, out object user);
            if (user == null) throw new _401Exception("You have not a registered token");
            else
            {                
                await _kshubService.KshubUserServices.AddUserAsync((KshubUser)user);
                return _mapper.Map<UserDetailDto>((KshubUser)user);
            }
        }
        [HttpPost]
        [Route("Signout")]
        public async ValueTask<UserDetailDto> SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //can null be mapped as a UserDetailDto
            //so why not return null dirextly?
            return null;
        }


        [HttpPost]
        [Route("Update")]
        [Authorize(Roles=KshubRoles.User)]
        public async ValueTask UpdateInfoAsync(UpdateUserDto updateUserDto)
        {
            //直接从cookie中获取Guid，不需要前端传递？
            //是否有被伪造的危险？——暂时没想到
            var id =Guid.Parse(HttpContext.User.Identity.Name);
            var definition = Builders<KshubUser>.Update
                .Set(t => t.Name, updateUserDto.Name)
                .Set(t => t.Introduction, updateUserDto.Introduction)
                .Set(t => t.AvatarUrl, updateUserDto.AvatarUrl);

            await _kshubService.KshubUserServices.UpDateAsync(id,definition);
        }
    }
}
