using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class Course : Entity, ISearchAble
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SearchAbleString { get; set; }
        public string CoverUrl { get; set; }

    }
}
