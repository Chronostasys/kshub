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
                cor.CourseId = courseDto.CourseId;
                cor.Name = courseDto.Name;
                cor.Description = courseDto.Description;
            }
            cor= await _kshubService.CourseServices.UpdateCourseAsync(cor);

            return _mapper.Map<CourseDetailDto>(cor);
        }
        [HttpPost]
        [Route("AddCourse")]
        [Authorize(Roles = KshubRoles.Admin)]
        public async ValueTask<CourseDetailDto> AddCourseAsync(CourseDto addCourseDto,string userId)
        {
            Course course = new Course
            {
                CourseId = addCourseDto.CourseId,
                Id = Guid.NewGuid(),
                Description = addCourseDto.Description,
                Name = addCourseDto.Name
            };
            await _kshubService.CourseServices.AddCourseAsync(course);
            var user=await _kshubService.KshubUserServices.FindUserAsync(userId);
            await _kshubService.UserInCourseService.AddOwnerInCouseAsync(course.Id, user.Id);
            return _mapper.Map<CourseDetailDto>(course);
        }

        [HttpPost]
        [Route("AddCourseAdmin")]
        public async ValueTask<KshubUserDetailDto> AddAdminAsync(Guid courseId,Guid addAdminId)
        {
            var owner= await _kshubService.KshubUserServices.FindUserAsync(HttpContext.User.Identity.Name);
            var user = await _kshubService.KshubUserServices.FindUserAsync(addAdminId);
            var curinfo =await _kshubService.UserInCourseService.GetInfoAsync(courseId, owner.Id);
            var addinfo = await _kshubService.UserInCourseService.GetInfoAsync(courseId, addAdminId);
            //当前用户不是对应course的owner或者当前用户不是该course的成员
            if (curinfo==null||!curinfo.Roles.Contains(CourseRoles.Owner))
            {
                throw new Exception("You have no access to add a admin!");
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
                return _mapper.Map<KshubUserDetailDto>(user);
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public async ValueTask<KshubUserDetailDto> AddUserAsync(int courseId,string userId)
        {
            var course = await _kshubService.CourseServices.FindCourseAsync(courseId);
            var user = await _kshubService.KshubUserServices.FindUserAsync(userId);
            await _kshubService.UserInCourseService.AddUserInCourseAsync(course.Id,user.Id);
            return _mapper.Map<KshubUserDetailDto>(user);
        }

        
    }
}
