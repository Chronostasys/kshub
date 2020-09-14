﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class Course : Entity, ISearchAble
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string CoverUrl { get; set; }

        public Guid BelongedCollegeId { get; set; }
        
        //允许课程有多名老师作为管理
        public List<Guid> TeachersId { get; set; }

        public List<Guid> StudentsID { get; set; }
        //老师用来指定各项成绩占比
        public Dictionary<string,double> ScoreRating { get; set; }
        public string SearchAbleString { get; set; }

    }
}
