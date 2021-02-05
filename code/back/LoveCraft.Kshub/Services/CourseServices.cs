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
            var course =await collection.Find(t => t.Id == courseId).FirstAsync();
            if (!course.TeacherIds.Remove(teacherId))
            {
                throw new _401Exception("No such Teacher in this course");
            }
            var update = Builders<Course>.Update.Set(t => t,course);
            await collection.UpdateOneAsync(t => t.Id == courseId,update);
        }
        public async ValueTask CreateTeacher(Guid courseId, Guid teacherId)
        {
            var course = await collection.Find(t => t.Id == courseId).FirstAsync();
            course.TeacherIds.Add(teacherId);
            var update = Builders<Course>.Update.Set(t => t, course);
            await collection.UpdateOneAsync(t => t.Id == courseId, update);
        }
        public async ValueTask<List<Guid>> GetTeacherIdsAsync(Guid courseId)
        {
            var course = await collection.Find(t => t.Id == courseId).FirstAsync();
            return course.TeacherIds;
        }
    }
}
