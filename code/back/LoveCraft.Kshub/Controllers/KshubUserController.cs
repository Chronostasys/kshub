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
namespace LoveCraft.Kshub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KshubUserController : Controller
    {
        private readonly KshubService _kshubService;
        readonly IMapper _mapper;
        IHostEnvironment env;
        public KshubUserController(KshubService kshubService,IMapper mapper, IHostEnvironment env)
        {
            _kshubService = kshubService;
            _mapper = mapper;
            this.env = env;
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
                StudentId = addUserDto.StudentId,
                Introduction=addUserDto.Introduction,
                Email=addUserDto.Email
            };
            await _kshubService.KshubUserServices.AddUserAsync(user);
            return _mapper.Map<KshubUserDetailDto>(user);
        }
    }
}
