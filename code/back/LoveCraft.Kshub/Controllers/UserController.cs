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
using LimFx.Business.Exceptions;
using DocumentFormat.OpenXml.Office2010.Excel;
using MongoDB.Driver;
using System.Security.Authentication;
using LimFx.Business.Models;

namespace LoveCraft.Kshub.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly KshubService _kshubService;
        readonly IMapper _mapper;
        IHostEnvironment env;
        public UserController(KshubService kshubService, IMapper mapper, IHostEnvironment env)
        {
            _kshubService = kshubService;
            _mapper = mapper;
            this.env = env;
        }

        [HttpGet]
        public async ValueTask<IEnumerable<UserDetailDto>> GetUsersAsync([FromQuery]Guid[] id)
        {
            var us = await _kshubService.KshubUserServices
                    .GetAsync(t => _mapper.Map<UserDetailDto>(t),0,100,
                    Builders<KshubUser>.Filter.In(u=>u.Id, id));
            return  us;
        }

        [HttpPost]
        [Route("Register")]
        public async ValueTask Register(AddUserDto addUserDto)
        {
            addUserDto.Email = addUserDto.Email.Trim();
            addUserDto.UserId = addUserDto.UserId.Trim();
            if (string.IsNullOrEmpty(addUserDto.Email)|| string.IsNullOrEmpty(addUserDto.UserId))
            {
                throw new _400Exception("Email or UserId cannot be empty!");
            }
            var user = _mapper.Map<KshubUser>(addUserDto);
            user.Roles = new List<string> { Roles.User };
            user.PassWordHash = addUserDto.Password;
            await _kshubService.KshubUserServices.AddUserWithCheckAsync(user);

        }

        [HttpPost]
        [Route("AddTeacher")]
        public async ValueTask AddTeacherAsync(AddUserDto addUserDto)
        {
            addUserDto.Email = addUserDto.Email.Trim();
            addUserDto.UserId = addUserDto.UserId.Trim();
            if (string.IsNullOrEmpty(addUserDto.Email) || string.IsNullOrEmpty(addUserDto.UserId))
            {
                throw new _400Exception("Email or TeacherId cannot be empty!");
            }
            var user = _mapper.Map<KshubUser>(addUserDto);
            user.Roles = new List<string> { KshubRoles.User,KshubRoles.Teacher };
            user.PassWordHash = addUserDto.Password;
            await _kshubService.KshubUserServices.AddUserWithCheckAsync(user);

        }
        [HttpPost]
        [Route("AddCollegeAdmin")]
        public async ValueTask AddCollegeAdmin(AddUserDto addUserDto)
        {
            addUserDto.Email = addUserDto.Email.Trim();
            addUserDto.UserId = addUserDto.UserId.Trim();
            if (string.IsNullOrEmpty(addUserDto.Email) || string.IsNullOrEmpty(addUserDto.UserId))
            {
                throw new _400Exception("Email or CollegeAdminId cannot be empty!");
            }
            var user = _mapper.Map<KshubUser>(addUserDto);
            user.Roles = new List<string> { KshubRoles.User, KshubRoles.CollegeAdmin };
            user.PassWordHash = addUserDto.Password;
            await _kshubService.KshubUserServices.AddUserWithCheckAsync(user);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
        public async ValueTask<UserDetailDto> LogIn(LogInDto logInDto,bool rememberMe)
        {
            var auth = User.Identity.IsAuthenticated;
            if (!auth)
            {
                return _mapper.Map<UserDetailDto>(await _kshubService.KshubUserServices.SignInAsAnonymous(HttpContext));
            }
            
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
                try
                {
                    
                    var user = await _kshubService.KshubUserServices.FindUserAsync(logInDto.UserId);
                    user.PassWordHash = logInDto.Password;
                    await _kshubService.KshubUserServices.LogInAsync(user, HttpContext, rememberMe);
                    return _mapper.Map<UserDetailDto>(user);

                }
                catch
                {
                    throw new _400Exception("Username or Password is wrong.");
                }
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
        public async ValueTask UpdateInfoAsync(UpdateUserDto updateUserDto)
        {

            var id =Guid.Parse(HttpContext.User.Identity.Name);
            var IsUser =(await _kshubService.KshubUserServices.GetAsync(
                t=>t.Roles.Contains("User"),0,null,t=>t.Id==id)).First();
            if (IsUser)
            {
                var definition = Builders<KshubUser>.Update
                    .Set(t => t.Name, updateUserDto.Name)
                    .Set(t => t.Introduction, updateUserDto.Introduction)
                    .Set(t => t.AvatarUrl, updateUserDto.AvatarUrl);

                await _kshubService.KshubUserServices.UpDateAsync(id, definition);
            }
            else
            {
                
                throw new _400Exception("Anonymous cannot change infomation!");
            }
        }

        [HttpGet]
        [Route("GetUsers")]
        public async ValueTask<IEnumerable<UserDetailDto>> GetUsersAsync
            (int page=0,int pagesize=10,bool IsDecsending = true)
        {
            //sort by Name
            return await _kshubService.KshubUserServices
                .GetAsync(t => _mapper.Map<UserDetailDto>(t), page, pagesize,"Name",IsDecsending
                , filter:null);
        }
    }
}
