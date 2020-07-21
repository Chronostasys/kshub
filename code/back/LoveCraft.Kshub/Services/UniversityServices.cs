using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using LimFx.Business.Exceptions;
using LoveCraft.Kshub.Models;
using MongoDB.Driver;

namespace LoveCraft.Kshub.Services
{
    public class UniversityServices:DbQueryServices<University>
    {
        public UniversityServices(IDatabaseSettings settings)
            : base(settings, settings.UniversityCollection) { }

        private async ValueTask CheckUniNameAsync(string name)
        {
            bool exist = false;
            try
            {
                var filter = Builders<University>.Filter.Eq(t => t.Name, name);
                var re = collection.Find(filter).First();
                exist = true;
            }
            catch
            {

            }
            if (exist)
            {
                throw new _401Exception("This uni exists!");
            }
        }
        public async ValueTask<University> AddUniWithCheckAsync(University university)
        {
            await CheckUniNameAsync(university.Name);
            await collection.InsertOneAsync(university);
            return university;
        }

        public async ValueTask<University> GetUniversityAsync(Guid id)
        {
            var filter = Builders<University>.Filter.Eq(t => t.Id, id);
            try
            {
                var result = await collection.Find(filter).FirstAsync();
                return result;
            }
            catch
            {
                throw new _401Exception("Cannot find this University");
            }
        }
        
        
    }
}
