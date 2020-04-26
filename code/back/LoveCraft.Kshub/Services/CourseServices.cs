using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveCraft.Kshub.Models;
using MongoDB.Bson;
using MongoDB.Driver;
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
    }
}
