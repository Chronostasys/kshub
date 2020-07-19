using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveCraft.Kshub.Models;
using LoveCraft.Kshub.Services;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using OpenXmlPowerTools;
using LoveCraft.Kshub.Dto;
using System.IO;
using Microsoft.AspNetCore.Http;
using LimFx.Business.Exceptions;

namespace LoveCraft.Kshub.Controllers
{
    public class FakeController:Controller
    {
        private KshubService _kshubService { get;}
        private IHostEnvironment _env { get; }
        private IMapper _mapper { get; }
        public FakeController(KshubService kshubService,IHostEnvironment env,IMapper mapper)
        {
            _kshubService = kshubService;
            _env = env;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("AddFakeUser")]
        public async ValueTask AddFakeUser()
        {
            for (int i = 0; i < 20; i++)
            {
                await _kshubService.KshubUserServices.AddUserWithCheckAsync(
                    new KshubUser
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test" + i.ToString(),
                        UserId = (100 + i).ToString(),
                        Email = (100 + i).ToString() + "@test.com",
                        PassWordHash = "12345678a",
                        IsEmailConfirmed=true,
                        Roles = new List<string> { "User" },
                    }); 
            }
        }

        [HttpPost]
        [Route("LoadFile")]
        public async ValueTask UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new _400Exception("You haven't choose a file");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

        }

        [HttpPost]
        [Route("AddFakeCourses")]
        public async ValueTask AddCourses()
        {
            //全部课程的管理员全部为UserId 为100的用户
            for(int i = 0; i < 10; i++)
            {
                var course = new Course
                {
                    CourseId = await _kshubService.CourseServices.GernerateCourseIdAsync(),
                    Id = Guid.NewGuid(),
                    Name = "Course" + i.ToString()
                };
                await _kshubService.CourseServices.AddCourseAsync(course);
                var stuid =await _kshubService.KshubUserServices.FindFirstAsync(t => t.UserId == 100.ToString(), u => u.Id);
                await _kshubService.UserInCourseService.AddAdminInCouseAsync(course.Id, stuid);
            }
        }
        [HttpPost]
        [Route("SendEmail")]
        public async ValueTask SendEmailConfirm(string email)
        {
            var emailProperty = new EmailProperty()
            {
                RazorTemplatePath = "\\EmailTemplate\\EmailConfirm.cshtml",
                Subject = "Confirm Kshub Account's Email",
                Receivers = new List<string> { email },
                Url = Url.Content($"{Request.Scheme}://{Request.Host.Value}/api/KshubUser/ValidateEmail/")
            };
            await _kshubService.EmailService.SendEmailAsync(emailProperty);
        }
    }
}
