using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LoveCraft.Kshub.Dto
{
    public class UniDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public string CoverUrl { get; set; }

    }
    public class AddUnilDto
    {
        public string Name { get; set; }
        public string Desciption { get; set; }
        public string CoverUrl { get; set; }

    }

}
