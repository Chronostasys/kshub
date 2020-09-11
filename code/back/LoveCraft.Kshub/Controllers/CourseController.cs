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
        public async ValueTask AddStudentAsync(Guid CourseId,List<Guid> StuIds)
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

        [HttpPost]
        [Route("SetScoreRating")]
        public async ValueTask<Dictionary<string,int>> SetScoreRatingAsync(Dictionary<string, int> scoreRule)
        {
            //一门Course的Manager可以设置评分细则
            //Dictionary好像不太好用，因为我还需要加一个关于本评分标准的Description
            //=>不过我可以在另外一个地方新加一个字段就是了，这点不急
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("DeleteCourse")]
        public async ValueTask DeleteCourseAsync(Guid guid)
        {
            throw new NotImplementedException();

        }

        [HttpDelete]
        [Route("RemoveCourseManager")]
        public async ValueTask RemoveCourseManagerAsync(Guid guid)
        {
            throw new NotImplementedException();

        }

        [HttpPut]
        [Route("EditInfo")]
        //Course的Manager可以修改的内容
        public async ValueTask<CourseDetailDto> UpdateCourseInfoAsync(UpdateCourseDto updateCourseDto)
        {
            //Course的Manager可以修改一些基础信息
            //评分标准就调用另外一个API？
            //==>提供CoureseManager能修改全部他能修改内容的Api
            //剩下怎么设置前端可以只用一部分
            throw new NotImplementedException();
        }

    }
}
