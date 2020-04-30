using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class UserInCourse:Entity,ISearchAble
    {
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public List<string> Roles { get; set; }
        public string SearchAbleString { get; set; }
    }
}
