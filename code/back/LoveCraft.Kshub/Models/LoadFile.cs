using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class LoadFile:Entity,ISearchAble
    {
        public byte[] Contents { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SearchAbleString { get; set; }
    }
}
