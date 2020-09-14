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
        
        public Guid CollegeId { get; set; }

        public string Name { get; set; }

        //Different from User's Id(Guid type),and used to login as Accout
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
