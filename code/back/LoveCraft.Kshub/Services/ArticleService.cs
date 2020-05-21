using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveCraft.Kshub.Models;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace LoveCraft.Kshub.Services
{
    public class ArticleService : DbQueryServices<Article>
    {
        public ArticleService(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.ArticleCollection) { }

        public async ValueTask<List<Article>> GetArticles(Guid userId)
        {
            return (await collection.FindAsync(t => t.AuthorId == userId)).ToList();
        }

    }
}
