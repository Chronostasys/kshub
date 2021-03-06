﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Dto
{
    public class KsDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string Abstract { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime{ get; set; }
        public List<string> Keywords { get; set; }

        public string ProjectUrl { get; set; }

        public Guid CollegeId { get; set; }
        public Guid CourseId{get;set;}
        public Guid CLassId{get;set;}
        public Guid ProjectManager { get; set; }
        public List<Guid> Participants { get; set; }
    }
    public class AddKsDto
    {
        public Guid CourseId{get;set;}
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string Abstract { get; set; }
        public string Description { get; set; }
        public List<string> Keywords { get; set; }
        public string ProjectUrl { get; set; }
        public List<Guid> Participants { get; set; }
    }
}
