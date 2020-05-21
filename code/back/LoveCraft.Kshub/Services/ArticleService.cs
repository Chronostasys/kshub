using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveCraft.Kshub.Models;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

//从本Service开始，查询时如果没有找到相应的内容不再返回null
//而是直接抛出exception，在controller里面进行处理
namespace LoveCraft.Kshub.Services
{
    public class ArticleService : DbQueryServices<Article>
    {
        public ArticleService(IDatabaseSettings databaseSettings)
            : base(databaseSettings, databaseSettings.ArticleCollection) { }

        public async ValueTask<List<Article>> GetAutherArticleAsync(Guid userId)
        {
            return (await collection.FindAsync(t => t.AuthorId == userId)).ToList();
        }
        public async ValueTask<Article> GetArticleAsync(Guid articleId)
        {
            return await (await collection.FindAsync(t => t.Id == articleId)).FirstAsync();
        }
    }
}
