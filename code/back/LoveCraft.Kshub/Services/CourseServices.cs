using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using LoveCraft.Kshub.Dto;
using LoveCraft.Kshub.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Services
{
    public class CourseServices:DbQueryServices<Course>
    {
        public CourseServices(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.CourseCollection) { }


        /// <summary>
        /// 假定要加入的Course未存在与当前College
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async ValueTask<Course> AddCourseWithoutCheckingAsync(Course course)
        {
            await collection.InsertOneAsync(course);
            return course;
        }
    }
}
