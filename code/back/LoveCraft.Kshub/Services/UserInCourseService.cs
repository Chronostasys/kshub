using LoveCraft.Kshub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveCraft.Kshub.Models;
namespace LoveCraft.Kshub.Services
{
    public class UserInCourseService:DbQueryServices<UserInCourse>
    {
        public UserInCourseService(IDatabaseSettings settings)
            : base(settings, settings.ConnectionString) { }
        public async ValueTask AddUserInCourseAsync(Guid courseId,Guid userId)
        {
            var piece = new UserInCourse
            {
                CourseId = courseId,
                UserId = userId,
                Roles = { CourseRoles.User }
            };
            await collection.InsertOneAsync(piece);
        }
        public async ValueTask AddAdminInCouseAsync(Guid courseId, Guid userId)
        {
            var piece = new UserInCourse
            {
                CourseId = courseId,
                UserId = userId,
                Roles = { CourseRoles.User, CourseRoles.Admin }
            };
            await collection.InsertOneAsync(piece);

        }
    }
}
