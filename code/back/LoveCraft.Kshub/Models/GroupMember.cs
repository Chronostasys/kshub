using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    /// <summary>
    /// GroupMember is designed for storing relationship in Course
    /// It contains University,College,Major,Grade,Class and StuId
    /// </summary>
    public class GroupMember
    {
        //When to get infomation in Course Group,return those directly is better
        //than a few Guids which have to query from db again
        public string UniversityName { get; set; }
        public string CollegeName { get; set; }
        public string Major { get; set; }
        public int Grade { get; set; }
        public string Class { get; set; }
        public Guid StuId { get; set; }
    }
}
