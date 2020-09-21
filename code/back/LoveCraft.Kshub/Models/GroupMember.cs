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
        public string University { get; set; }
        public string College { get; set; }
        public string Major { get; set; }
        public int Grade { get; set; }
        public string Class { get; set; }
        public Guid StuId { get; set; }
    }
}
