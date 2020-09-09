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
using LimFx.Business.Exceptions;
using Castle.Core.Internal;
using DocumentFormat.OpenXml.InkML;

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
        
        [HttpPost]
        [Route("AddCourse")]
        public async ValueTask AddCourseAsync(AddCourseDto addCourseDto)
        {
            
            var course = _mapper.Map<Course>(addCourseDto);
            //显然不应该有这句
            //course.TeacherIds.Add(Guid.Parse(User.Identity.Name));
            course.Id = Guid.NewGuid();
            try
            {
                await _kshubService.CollegeServices.FindFirstAsync(course.BelongedCollegeId);
            }
            catch
            {
                throw new _401Exception("You can't add this Course because the belonged College doesn't exist!");
            }

            var filter = Builders<Course>.Filter.Eq(t => t.BelongedCollegeId, course.BelongedCollegeId)
                & Builders<Course>.Filter.Eq(t=>t.Name,course.Name);
            var re=await _kshubService.CourseServices.GetAsync(t => t, 0, 1,"UpdateTime", true, filter);
            if (re.IsNullOrEmpty())
            {
                await _kshubService.CourseServices.AddCourseWithoutCheckingAsync(course);
            }
            else
            {
                throw new _401Exception("This Course has already existed!");
            }
        }

        [HttpGet]
        [Route("GetCourses")]
        public async ValueTask<CourseDetailDto> GetCourseAsync(int page = 0, int pagesize = 10)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetCourse")]
        public async ValueTask<CourseDetailDto> GetCourseAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("AddStudent")]
        public async ValueTask AddStudentAsync(Guid CourseId,Guid StuId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("AddTeacher")]
        //主要添加老师还是在创建课程的时候添加吧
        public async ValueTask AddTeacherAsync()
        {
            throw new NotImplementedException();
        }


    }
}
