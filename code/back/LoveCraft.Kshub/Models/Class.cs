using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class Class:Entity,ISearchAble
    {
        public string Name { get; set; }
        public Guid CollegeId { get; set; }
        public string SearchAbleString { get; set; }
    }
}
