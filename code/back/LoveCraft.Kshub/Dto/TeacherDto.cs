using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Dto
{
    public class AddTeacherDto
    {
        public Guid CourseId { get; set; }

        public Guid TeacherId { get; set; }
    }
    public class GetTeacherDto
    {
        public Guid CourseId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool IsDecsending { get; set; }
    }
}
