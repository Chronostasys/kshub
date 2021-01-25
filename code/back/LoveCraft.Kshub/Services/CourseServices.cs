using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using LimFx.Business.Exceptions;
using LoveCraft.Kshub.Dto;
using LoveCraft.Kshub.Models;
using Lucene.Net.Util.Fst;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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
        /// Add a course which not exist in current college
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async ValueTask<Course> AddCourseWithoutCheckingAsync(Course course)
        {
            await collection.InsertOneAsync(course);
            return course;
        }
        public async ValueTask DeleteCourseAsync(Guid guid)
        {
            var filter = Builders<Course>.Filter.Eq(t => t.Id, guid);
            await collection.DeleteOneAsync(filter);
        }
        public async ValueTask RemoveTeacherAsync(Guid courseId,Guid teacherId)
        {
            var teachers =await collection.Find(t => t.Id == courseId).FirstAsync();
            if (!teachers.TeachersId.Remove(teacherId))
            {
                throw new _401Exception("No such Teacher in this course");
            }
            var update = Builders<Course>.Update.Set(t => t,teachers);
            await collection.UpdateOneAsync(t => t.Id == courseId,update);
        }
    }
}
