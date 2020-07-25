using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LimFx.Business.Models;
namespace LoveCraft.Kshub.Models
{
    //这里展示了数据库中的collection名，然后之后把类映射到上面去
    public class MongoDbSettings : IDatabaseSettings
    {
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
        public string CollegeCollection { get; set; }
        public string FileCollection { get; set; }
        public string CourseCollection { get; set; }
        public string UserCollection { get; set; }
        public string ClassCollection { get; set; }
        public string UniversityCollection { get; set; }
    }

}
