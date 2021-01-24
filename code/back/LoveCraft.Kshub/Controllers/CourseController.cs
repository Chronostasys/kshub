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
using Lucene.Net.Store;

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
        public async ValueTask<Guid> AddCourseAsync(AddCourseDto addCourseDto)
        {
            
            var course = _mapper.Map<Course>(addCourseDto);
            course.Id = Guid.NewGuid();
            var collegeAdminId=Guid.Parse(User.Identity.Name);
            await _kshubService.KshubUserServices.CheckAuthAsync(KshubRoles.CollegeAdmin, collegeAdminId);
            var collegeId = await _kshubService.KshubUserServices.FindFirstAsync(t => t.Id == collegeAdminId, t => t.CollegeId);

            var collegeName = await _kshubService.CollegeServices.FindFirstAsync(t => t.Id == collegeId, t => t.Name);
            var uniId = await _kshubService.CollegeServices.FindFirstAsync(t => t.Id == collegeId, t => t.BelongUniId);
            var uniName = await _kshubService.UniversityServices.FindFirstAsync(t => t.Id == uniId, t => t.Name);
            course.Groups = new List<GroupMember>();

            course.Groups.Add(new GroupMember
                {
                    CollegeName = collegeName,
                    UniversityName = uniName,
                });
            var filter = Builders<Course>.Filter.Eq(t => t.BelongedCollegeId, course.BelongedCollegeId)
                & Builders<Course>.Filter.Eq(t=>t.Name,course.Name);
            var re=await _kshubService.CourseServices.GetAsync(t => t, 0, 1,"UpdateTime", true, filter);
            if (re.IsNullOrEmpty())
            {
                await _kshubService.CourseServices.AddCourseWithoutCheckingAsync(course);
                return course.Id;
            }
            else
            {
                throw new _401Exception("This Course has already existed!");
            }
        }

        [HttpGet]
        [Route("GetCourses")]
        public async ValueTask<IEnumerable<CourseDetailDto>> GetCourseAsync(int page = 0, int pagesize = 10,bool IsAscending=true)
        {
            var filter = Builders<Course>.Filter.Empty;
            SortDefinition<Course> sort;
            if (IsAscending)
            {
                sort = Builders<Course>.Sort.Ascending(t => t.Name);
            }
            else
            {
                sort = Builders<Course>.Sort.Descending(t => t.Name);
            }
            var t = await _kshubService.CourseServices.GetAsync(t=>_mapper.Map<CourseDetailDto>(t),page, pagesize);
            return t;
        }

        [HttpGet]
        [Route("GetCourse")]
        public async ValueTask<CourseDetailDto> GetCourseAsync(string id)
        {
            Guid guid = Guid.Parse(id);
            var filter = Builders<Course>.Filter.Eq(t => t.Id, guid);
            var course= await _kshubService.CourseServices.FindFirstAsync(guid);
            return _mapper.Map<CourseDetailDto>(course);
        }

        [HttpPost]
        [Route("AddStudent")]
        public async ValueTask AddStudentAsync(Guid CourseId,List<Guid> StuIds)
        {
            var currentId =Guid.Parse(User.Identity.Name);
            await _kshubService.KshubUserServices.CheckAuthAsync(KshubRoles.CollegeAdmin, currentId);
            var update = Builders<Course>.Update.PushEach(t=>t.StudentsID,StuIds);

            await _kshubService.CourseServices.UpDateAsync(CourseId, update);
        }


        [HttpPut]
        [Route("SetScoreRating")]
        public async ValueTask<Dictionary<string,double>> SetScoreRatingAsync(Guid courseId,Dictionary<string, double> scoreRule)
        {
            await _kshubService.KshubUserServices.CheckAuth(HttpContext, KshubRoles.Teacher);
            var filter = Builders<Course>.Filter.Eq(t => t.Id, courseId);
            var teacherIds = await _kshubService.CourseServices.FindFirstAsync(t=>t.Id==courseId,t=>t.TeachersIds);
            if (!teacherIds.Contains(Guid.Parse(User.Identity.Name))){
                throw new _403Exception("You can't set ScoreRating");
            }

            double score = 0;
            foreach(var item in scoreRule)
            {
                score += item.Value;
            }
            if (score != 100)
            {
                throw new _400Exception("Invaild Score Rating");
            }

            var update = Builders<Course>.Update.Set(t => t.ScoreRating, scoreRule);
            await _kshubService.CourseServices.UpDateAsync(courseId, update);
            return scoreRule;
        }

        [HttpDelete]
        [Route("DeleteCourse")]
        public async ValueTask DeleteCourseAsync(Guid guid)
        {
            //It's useless to set a dustbin,so delete it from db directly
            await _kshubService.CourseServices.DeleteCourseAsync(guid);
        }

        [HttpDelete]
        [Route("RemoveCourseManager")]
        public async ValueTask RemoveCourseTeacherAsync(Guid courseId,Guid teacherId)
        {
            await _kshubService.KshubUserServices.CheckAuth(HttpContext, KshubRoles.CollegeAdmin);
            await _kshubService.CourseServices.RemoveTeacherAsync(courseId, teacherId);
        }

        [HttpPut]
        [Route("UpdateCourseInfo")]
        //update something by teacher
        public async ValueTask UpdateCourseInfoAsync(UpdateCourseDto updateCourseDto)
        {

            var update = Builders<Course>.Update
                        .Set(t => t.Description, updateCourseDto.Description)
                        .Set(t => t.CoverUrl, updateCourseDto.CoverUrl)
                        .Set(t => t.ScoreRating, updateCourseDto.ScoreRating);
            await _kshubService.CourseServices.UpDateAsync(updateCourseDto.Id, update);
        }

        [HttpGet]
        [Route("GetStudents")]
        public async ValueTask GetStudentsAsync()
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("GetTeachers")]
        public async ValueTask<IEnumerable<UserDetailDto>> GetTeachersAsync(GetTeacherDto getTeacherDto)
        {
            var teacherIds =await _kshubService.CourseServices.GetTeacherIdsAsync(getTeacherDto.CourseId);
            List<UserDetailDto> lists = new List<UserDetailDto>();
            foreach(var item in teacherIds)
            {
                var Dto = _mapper.Map<UserDetailDto>(await _kshubService.KshubUserServices.GetUserById(item));
                lists.Add(Dto);
            }
            return lists;
        }
        [HttpPost]
        [Route("AddTeacher")]
        //Add teacher in Update
        public async ValueTask AddTeacherAsync(AddTeacherDto addTeacherDto)
        {
            await _kshubService.CourseServices.CreateTeacher(addTeacherDto.CourseId, addTeacherDto.TeacherId);
        }
    }
}
