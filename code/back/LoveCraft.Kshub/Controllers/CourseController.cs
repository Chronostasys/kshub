using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using LoveCraft.Kshub.Services;
using LoveCraft.Kshub.Dto;
using Microsoft.AspNetCore.Authorization;
using LoveCraft.Kshub.Models;
using MongoDB.Driver;
using LoveCraft.Kshub.Exceptions;
namespace LoveCraft.Kshub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly KshubService _kshubService;
        readonly IMapper _mapper;
        IHostEnvironment env;
        public CourseController(KshubService kshubService, IMapper mapper, IHostEnvironment hostEnvironment)
        {
            _kshubService = kshubService;
            _mapper = mapper;
            env = hostEnvironment;
        }

        [HttpGet]
        [Route("GetCourse")]
        public async ValueTask<CourseDetailDto> GetCourseAsync(int courseId)
        {
            var co = await _kshubService.CourseServices.FindCourseAsync(courseId);
            return _mapper.Map<CourseDetailDto>(co);
        }

        [HttpGet]
        [Route("GetCourses")]
        public async ValueTask<List<CourseDetailDto>> GetCourseAsync(string name)
        {
            var cos = await _kshubService.CourseServices.FindCourseAsync(name);
            List<CourseDetailDto> listdto = new List<CourseDetailDto>();
            foreach (var item in cos)
            {
                listdto.Add(_mapper.Map<CourseDetailDto>(item));
            }
            return listdto;
        }

        [HttpPost]
        [Route("Update")]
        public async ValueTask<CourseDetailDto> UpdateCourseAsync(Guid courseId,CourseDto courseDto)
        {
            var user = await _kshubService.KshubUserServices.FindUserAsync(User.Identity.Name);
            var p=await _kshubService.UserInCourseService.GetInfoAsync(courseId, user.Id);
            var cor = await _kshubService.CourseServices.FindCourseAsync(courseId);

            if (p.Roles.Contains(CourseRoles.Owner))
            {
                
                cor.Name = courseDto.Name;
                cor.Description = courseDto.Description;
            }
            cor= await _kshubService.CourseServices.UpdateCourseAsync(cor);

            return _mapper.Map<CourseDetailDto>(cor);
        }
        [HttpPost]
        [Route("AddCourse")]
        public async ValueTask<CourseDetailDto> AddCourseAsync(CourseDto addCourseDto,string userId)
        {
            if (addCourseDto.Name.Equals("CourseIdRecord"))
            {
                throw new Exception("Unlegal Course Name!");
            }
            var isAdmin = await _kshubService.KshubUserServices.FindFirstAsync(t=>t.Id==Guid.Parse(User.Identity.Name), u => u.Roles.Contains(KshubRoles.Admin));
            if (isAdmin)
            {
                Course course = new Course
                {
                    CourseId =await _kshubService.CourseServices.GernerateCourseIdAsync(),
                    Id = Guid.NewGuid(),
                    Description = addCourseDto.Description,
                    Name = addCourseDto.Name
                };
                await _kshubService.CourseServices.AddCourseAsync(course);
                var user = await _kshubService.KshubUserServices.FindUserAsync(userId);
                await _kshubService.UserInCourseService.AddOwnerInCouseAsync(course.Id, user.Id);
                return _mapper.Map<CourseDetailDto>(course);

            }
            else
            {
                throw new _403Exception("Only admin can add Course!");
            }
        }

        [HttpPost]
        [Route("AddCourseAdmin")]
        public async ValueTask<UserDetailDto> AddAdminAsync(Guid courseId,Guid addAdminId)
        {
            var owner= await _kshubService.KshubUserServices.FindUserAsync(HttpContext.User.Identity.Name);
            var user = await _kshubService.KshubUserServices.FindUserAsync(addAdminId);
            var curinfo =await _kshubService.UserInCourseService.GetInfoAsync(courseId, owner.Id);
            var addinfo = await _kshubService.UserInCourseService.GetInfoAsync(courseId, addAdminId);
            //当前用户不是对应course的owner或者当前用户不是该course的成员
            if (curinfo==null||!curinfo.Roles.Contains(CourseRoles.Owner))  
            {
                throw new _403Exception("You have no access to add a admin!");
            }
            else
            {
                if (addinfo == null)
                {
                    await _kshubService.UserInCourseService.AddAdminInCouseAsync(courseId, addAdminId);
                }
                else
                {
                    addinfo.Roles.Add(CourseRoles.Admin);
                    await _kshubService.UserInCourseService.UpdateAsync(addinfo);
                }
                return _mapper.Map<UserDetailDto>(user);
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public async ValueTask<UserDetailDto> AddUserAsync(int courseId,string userId)
        {
            var course = await _kshubService.CourseServices.FindCourseAsync(courseId);
            var user = await _kshubService.KshubUserServices.FindUserAsync(userId);
            await _kshubService.UserInCourseService.AddUserInCourseAsync(course.Id,user.Id);
            return _mapper.Map<UserDetailDto>(user);
        }
        

    }
}
