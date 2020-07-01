using LimFx.Business.Exceptions;
using LoveCraft.Kshub.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Services
{
    public class LoadFileServices:DbQueryServices<LoadFile>
    {

        public LoadFileServices(IDatabaseSettings databaseSettings)
            :base(databaseSettings,databaseSettings.FileCollection)
        {

        }
        public async ValueTask<Guid> LoadFileAsync(IFormFile infile)
        {
            using(var memory=new MemoryStream())
            {
                await infile.CopyToAsync(memory);
                //file less than 2M
                if(memory.Length< 2097152)
                {
                    var file = new LoadFile
                    {
                        Id = Guid.NewGuid(),
                        Name = infile.Name,
                        Contents = memory.ToArray()
                    };
                    await collection.InsertOneAsync(file);
                    return file.Id;
                }
                else
                {
                    throw new _401Exception("The file is too large,please load a file which less than 2M");
                }

            }
        }
    }
}
