using LoveCraft.Kshub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Services
{
    public class ClassServices:DbQueryServices<Class>
    {
        public ClassServices(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.ClassCollection) { }


    }
}
