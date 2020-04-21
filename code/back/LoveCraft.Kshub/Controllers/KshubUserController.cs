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
namespace LoveCraft.Kshub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KshubUserController : Controller
    {
        private readonly KshubService _kshubService;
        readonly IMapper _mapper;
        IHostEnvironment env;
        public KshubUserController(KshubService kshubService, IMapper mapper, IHostEnvironment env)
        {
            _kshubService = kshubService;
            _mapper = mapper;
            this.env = env;
        }

        [HttpGet("{id}")]
        public async ValueTask<KshubUserDetailDto> GetUsersAsync(string id)
        {
            var user = await _kshubService.KshubUserServices.FindUserAsync(id);
            return _mapper.Map<KshubUserDetailDto>(user);
        }
        
        [HttpPost]
        [Route("AddUser")]
        public async ValueTask<KshubUserDetailDto> Register(AddUserDto addUserDto)
        {
            var user = new KshubUser
            {
                Id = Guid.NewGuid(),
                Name = addUserDto.Name,
                SchoolName = addUserDto.SchoolName,
                UserId = addUserDto.StudentId,
                Introduction = addUserDto.Introduction,
                Email = addUserDto.Email,
                PassWordHash = addUserDto.Password,
                Roles =new List<string> { "User"},
            };
            await _kshubService.KshubUserServices.AddUserAsync(user);
            return _mapper.Map<KshubUserDetailDto>(user);
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
        public async ValueTask<KshubUserDetailDto> LogIn(LogInDto logInDto,bool rememberMe)
        {
            var user = await _kshubService.KshubUserServices.FindUserAsync(logInDto.StudentId);

            //var password = _kshubService.KshubUserServices.HashPasswordWithSalt(logInDto.Password);
            if (user == null)
            {
                throw new Exception("Username or Password is wrong.");
            }
            else
            {
                user.PassWordHash = logInDto.Password;
                await _kshubService.KshubUserServices.LogInAsync(user, HttpContext,rememberMe);
            }
            return _mapper.Map<KshubUserDetailDto>(user);
        }
    }
}
