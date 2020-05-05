using LoveCraft.Kshub.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Services
{
    public class UserInCourseService:DbQueryServices<UserInCourse>
    {
        public UserInCourseService(IDatabaseSettings settings)
            : base(settings, settings.ConnectionString) { }
        public async ValueTask<UserInCourse> GetInfoAsync(Guid courseId, Guid userId)
        {
            var piece = await (await collection.FindAsync(t => t.CourseId == courseId && t.UserId == userId))
                .FirstOrDefaultAsync();
            return piece;
        }
        public async ValueTask<UserInCourse> UpdateAsync(UserInCourse userInCourse)
        {
            await UpDateAsync(userInCourse.Id, Builders<UserInCourse>.Update
                .Set(t => t.Roles, userInCourse.Roles));
            return userInCourse;
        }
        public async ValueTask<UserInCourse> AddUserInCourseAsync(Guid courseId,Guid userId)
        {
            var p = await GetInfoAsync(courseId, userId);
            if (p == null)
            {
                var piece = new UserInCourse
                {
                    CourseId = courseId,
                    UserId = userId,
                    Roles = { CourseRoles.User }
                };
                await collection.InsertOneAsync(piece);
                return piece;
            }
            else throw new Exception("This user is alread in this course!");
            
        }
        public async ValueTask<UserInCourse> AddAdminInCouseAsync(Guid courseId, Guid userId)
        {
            var piece = new UserInCourse
            {
                CourseId = courseId,
                UserId = userId,
                Roles = { CourseRoles.User, CourseRoles.Admin }
            };
            await collection.InsertOneAsync(piece);
            return piece;
        }
        public async ValueTask<UserInCourse> AddOwnerInCouseAsync(Guid courseId, Guid userId)
        {
            var piece = new UserInCourse
            {
                CourseId = courseId,
                UserId = userId,
                Roles =new List<string>{ CourseRoles.User, CourseRoles.Admin,CourseRoles.Owner }
            };
            await collection.InsertOneAsync(piece);
            return piece;
        }

    }
}
