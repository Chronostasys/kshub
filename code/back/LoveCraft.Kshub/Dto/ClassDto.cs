using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Dto
{
    public class AddClassDto
    {
        public string Name { get; set; }
        public Guid CollegeId { get; set; }
    }
    public class ClassDetailDto
    {
        public string Name { get; set; }
        public Guid CollegeId { get; set; }
        public Guid UniversityId { get; set; }
    }
}
