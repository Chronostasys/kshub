using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using LimFx.Business.Exceptions;
using LoveCraft.Kshub.Dto;
using LoveCraft.Kshub.Models;
using MongoDB.Driver;

namespace LoveCraft.Kshub.Services
{
    public class UniversityServices:DbQueryServices<University>
    {
        public UniversityServices(IDatabaseSettings settings)
            : base(settings, settings.UniversityCollection)
        {
            List<CreateIndexModel<University>> indexes = new();
            var indexBuilder = Builders<University>.IndexKeys;
            indexes.Add(new(indexBuilder.Ascending(u=>u.Name), new(){Unique=true}));
            collection.Indexes.CreateManyAsync(indexes);
        }
        public async ValueTask<University> AddUniWithCheckAsync(University university)
        {
            try
            {
                await collection.InsertOneAsync(university);
            }
            catch (System.Exception e)
            {
                throw new _401Exception("This uni exists!", e);
            }
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
