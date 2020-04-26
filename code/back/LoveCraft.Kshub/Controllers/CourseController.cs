using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using LoveCraft.Kshub.Services;
using LoveCraft.Kshub.Dto;
namespace LoveCraft.Kshub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController:Controller
    {
        private readonly KshubService _kshubService;
        readonly IMapper _mapper;
        IHostEnvironment env;
        public CourseController(KshubService kshubService,IMapper mapper,IHostEnvironment hostEnvironment)
        {
            _kshubService = kshubService;
            _mapper = mapper;
            env = hostEnvironment;
        }
        
        [HttpGet]
        [Route("GetCourse")]
        public async ValueTask<CourseDetailDto> GetCourseAsync(int courseId)
        {
            var co=await _kshubService.CourseServices.FindCourseAsync(courseId);
            return _mapper.Map<CourseDetailDto>(co);
        }

        [HttpGet]
        [Route("GetCourses")]
        public async ValueTask<List<CourseDetailDto>> GetCourseAsync(string name)
        {
            var cos = await _kshubService.CourseServices.FindCourseAsync(name);
            List<CourseDetailDto> listdto = new List<CourseDetailDto>();
            foreach(var item in cos)
            {
                listdto.Add(_mapper.Map<CourseDetailDto>(item));
            }
            return listdto;
        }
    }
}
