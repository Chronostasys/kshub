using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Models
{
    public class Course : Entity, ISearchAble
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string CoverUrl { get; set; }

        public Guid BelongedCollegeId { get; set; }
        
        //All a few teacher to manager the same course 
        public List<Guid> TeachersId { get; set; }

        public List<Guid> StudentsID { get; set; }
        
        //specify the final score rating
        public Dictionary<string,double> ScoreRating { get; set; }
        public string SearchAbleString { get; set; }

    }
}
