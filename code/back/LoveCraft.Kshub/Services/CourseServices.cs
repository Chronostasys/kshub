using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveCraft.Kshub.Models;
using Microsoft.CodeAnalysis.CSharp;
using MongoDB.Bson;
using MongoDB.Driver;
using OpenXmlPowerTools;

namespace LoveCraft.Kshub.Services
{
    public class CourseServices : DbQueryServices<Course>
    {

        public CourseServices(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.CourseCollection) { }

        public async ValueTask<Course> FindCourseAsync(int courseId)
        {
            var co =await (await collection.FindAsync(item => item.CourseId == courseId)).FirstOrDefaultAsync();
            return co;
        }
        public async ValueTask<Course> FindCourseAsync(Guid guid)
        {
            var co = await (await collection.FindAsync(item => item.Id==guid)).FirstOrDefaultAsync();
            return co;
        }
        public async ValueTask<Course> UpdateAsync(Course course)
        {
            await UpDateAsync(course.Id, Builders<Course>.Update
                .Set(t => t.Name, course.Name)
                .Set(t => t.CourseId, course.CourseId)
                .Set(t => t.Description, course.Description));
            return course;
        }
        public async ValueTask<List<Course>> FindCourseAsync(string name)
        {
            var cos =(await collection.FindAsync(item => item.Name == name)).ToList();
            return cos;
        }
        public async ValueTask AddCourseAsync(Course course)
        {
            await collection.InsertOneAsync(course);
        }
        public async ValueTask<Course> UpdateCourseAsync(Course course)
        {
            var update = Builders<Course>.Update
                    .Set(t=>t, course);
            await collection.FindOneAndUpdateAsync(t=>t.Id==course.Id, update);
            return course;
        }

        /// <summary>
        /// Give you a safe and unique CourseId to use. 
        /// </summary>
        /// <returns></returns>
        public async ValueTask<int> GernerateCourseIdAsync()
        {
            //存储courseId最大的那个数，如果使用需要再+1
            var p = await (await collection.FindAsync(r => r.Name == "CourseIdRecord")).FirstOrDefaultAsync();
            if (p == null)
            {
                await collection.InsertOneAsync(new Course
                {

                    CourseId = 10000,
                    Name = "CourseIdRecord"
                });
                return 10000;
            }
            else
            {
                var n = await collection.FindOneAndUpdateAsync(t => t.Name == "CourseIdRecord",
                    Builders<Course>.Update.Set<int>(t=>t.CourseId,p.CourseId+1));
                return n.CourseId;
            }
        }
    }
}
