using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Dto
{
    public class UserDetailDto
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string SchoolName { get; set; }
        public string Introduction { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string AvatarUrl { get; set; }
    }
    public class AddUserDto
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string SchoolName { get; set; }
        public string Introduction { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }

        
        public Guid BelongId { get; set; }

    }
    public class LogInDto
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserDto
    {

        public string Name { get; set; }
        //schoolName不允许修改吧
        //public string SchoolName { get; set; }
        public string Introduction { get; set; }
        //修改密码应该需要另外写一个Api吧，一会参考下别的
        //public string PassWordHash { get; set; }
        public string AvatarUrl { get; set; }
    }
}
