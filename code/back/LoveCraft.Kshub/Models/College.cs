using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class College : Entity, ISearchAble
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string CoverUrl { get; set; }
        
        public Guid BelongUniId { get; set; }
        public string SearchAbleString { get; set; }

    }
}
