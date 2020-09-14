using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LimFx.Business.Models;
using LimFx.Business.Services;
namespace LoveCraft.Kshub.Models
{
    public class KshubUser : Entity, ISearchAble, IUser,IPraiseAble
    {
        //添加了一个用户属于的组织Id,如果是学生那么就是所属班级，若是老师则为所属CollegeId
        public Guid BelongId { get; set; }

        public string Name { get; set; }

        //用户自己的一个Id，不同于Guid，登陆时候的账户名
        public string UserId { get; set; }
        public string SchoolName { get; set; }
        public string Introduction { get; set; }
        public string PassWordHash { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string SearchAbleString { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public int Follows { get; set; }
        public int Followers { get; set; }
        public string AvatarUrl { get; set; }
        public int Awesomes { get; set; }
        public float Exp { get; set; }
        public string SecurityStamp { get; set; }
    }
}
