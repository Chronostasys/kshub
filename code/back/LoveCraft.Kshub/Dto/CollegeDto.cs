using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Dto
{
    public class AddCollegeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public Guid BelongUniId { get; set; }

    }
    public class CollegeDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public Guid BelongUniId { get; set; }

    }
}
