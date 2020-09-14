using LoveCraft.Kshub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Services
{
    public class KsServices:DbQueryServices<Ks>
    {
        public KsServices(IDatabaseSettings databaseSettings)
            : base(databaseSettings,databaseSettings.KsCollection) { }
    }
}
