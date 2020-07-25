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
    }
}
