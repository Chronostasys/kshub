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
        //specify the final score rating
        public Dictionary<string,double> ScoreRating { get; set; }
        public string SearchAbleString { get; set; }

        //This prosperity will store all Relationship 
        //such as Belonged University-College,the CollegeAdmin,
        //Teacher and the students who belong this course
        public List<GroupMember> Groups { get; set; }
        //Ks In this Course
        public List<Guid> KsList { get; set; }
        //-------------------------------------------------
        //the following prosperies will be removed when Groups finished 
        public Guid BelongedCollegeId { get; set; }
        //All a few teacher to manager the same course 
        public List<Guid> TeachersId { get; set; }
        public List<Guid> StudentsID { get; set; }
        //---------------------------------------------------
    }
}
