﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class University:Entity,ISearchAble
    {
        public string Name { get; set; }
        public string Desciption { get; set; }
        public string CoverUrl { get; set; }
        public string SearchAbleString { get; set; }
    }
}
