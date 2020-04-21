using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class KshubUser:Entity,ISearchAble
    {
        public string Name { get; set; }
        public string StudentId { get; set; }
        public string SchoolName { get; set; }
        public string Introduction { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<string> Role { get; set; }
        public string SearchAbleString { get; set; }
    }
}
