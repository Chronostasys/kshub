using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class Course : Entity, ISearchAble
    {
        public string Name { get; set; }
        public string Desciption { get; set; }

        public string CoverUrl { get; set; }

        public Guid BelongedCollegeId { get; set; }
        public Guid CourseManagerId { get; set; }

        //老师用来指定各项成绩占比
        public Dictionary<string,double> ScoreRating { get; set; }
        public string SearchAbleString { get; set; }

    }
}
