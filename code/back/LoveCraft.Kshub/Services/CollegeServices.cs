using DocumentFormat.OpenXml.Drawing.Charts;
using LimFx.Business.Exceptions;
using LoveCraft.Kshub.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Services
{
    public class CollegeServices:DbQueryServices<College>
    {
        public CollegeServices(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.CollegeCollection) { }

        public async ValueTask<College> AddCollegeAsync(College college)
        {
            try
            {
                var filter = Builders<College>.Filter.Eq(t => t.Name, college.Name)
                    & Builders<College>.Filter.Eq(t => t.BelongUniId, college.BelongUniId);


                await collection.Find(filter).FirstAsync();
                throw new _401Exception("This College exists!");
            }
            catch
            {
                await collection.InsertOneAsync(college);
                return college;
            }
        }
    }
}
