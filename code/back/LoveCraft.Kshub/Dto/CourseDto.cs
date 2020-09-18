using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Dto
{
    
    public class CourseDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string Description { get; set; }
        public List<Guid> TeacherIds { get; set; }
        public Dictionary<string, double> ScoreRating { get; set; }

    }
    public class AddCourseDto
    {
        public string CoverUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> TeacherIds { get; set; }

        //BelongedCollegeId可以直接从添加的管理员
        //public Guid BelongedCollegeId { get; set; }

        //这个东西显然应该让老师来设置，不应该让管理所有课程的学院管理员来使用
        //public Dictionary<string, double> ScoreRating { get; set; }
    }
    public class UpdateCourseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public Dictionary<string, double> ScoreRating { get; set; }

    }
}
