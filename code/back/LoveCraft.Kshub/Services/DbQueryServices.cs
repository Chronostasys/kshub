using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveCraft.Kshub.Models;
using MongoDB.Driver;
namespace LoveCraft.Kshub.Services
{
    //Limfx的包里面封装了基本方法，我还可以自己再写一点方法出来 :)

    public class DbQueryServices<T> :LimFx.Business.Services.DBQueryServices<T> where T:Entity, ISearchAble
    {
        public DbQueryServices(IDatabaseSettings settings, string collectionName)
            : base(settings.ConnectionString, settings.DatabaseName, collectionName) { }
    }
}
