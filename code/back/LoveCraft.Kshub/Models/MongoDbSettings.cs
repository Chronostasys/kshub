﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LimFx.Business.Models;
namespace LoveCraft.Kshub.Models
{

    public class MongoDbSettings : IDatabaseSettings
    {
        public string KsCollection { get; set; }

        public string UniversityCollection { get; set; }
        public string CollegeCollection { get; set; }
        public string ClassCollection { get; set; }
        public string FileCollection { get; set; }
        public string ArticleCollection { get; set; }
        public string CourseCollection { get; set; }
        public string UserCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IDatabaseSettings:IBaseDbSettings
    {
        public string KsCollection { get; set; }
        public string CollegeCollection { get; set; }
        public string FileCollection { get; set; }
        public string CourseCollection { get; set; }
        public string UserCollection { get; set; }
        public string ClassCollection { get; set; }
        public string UniversityCollection { get; set; }
    }

}
