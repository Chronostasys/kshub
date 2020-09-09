using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Dto
{
    //两个dtp相同，要不要使用一个Dto代替？有没有必要
    //如果注册时候需要填写的信息，以后在返回的时候也是这些信息，那我就可以将两个dto合并
    //那么至于这里，我认为可以合并
    public class CourseDetailDto
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string Description { get; set; }
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
}
