using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class Ks : Entity, ISearchAble
    {
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string Abstract { get; set; }
        public string Description { get; set; }
        public List<string> Keywords { get; set; }

        public string ProjectUrl { get; set; }
        //附件保存链接还是一个IFormFile对象，我觉得是url
        //文件应该会专门放到某个地方

        public Guid BelongCollegeId { get; set; }
        public Guid ProjectManager { get; set; }
        public List<Guid> Participants { get; set; }
        public string SearchAbleString { get; set; }

    }
}
