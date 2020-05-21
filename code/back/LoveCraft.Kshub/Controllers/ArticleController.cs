using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using LoveCraft.Kshub.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using LoveCraft.Kshub.Dto;
using LoveCraft.Kshub.Exceptions;
using Qiniu.Util;
using LoveCraft.Kshub.Models;

namespace LoveCraft.Kshub.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        private readonly KshubService _kshubService;
        public IMapper _mapper { get; set; }
        public IHostEnvironment env { get; set; }
        public ArticleController(KshubService kshubService,IMapper mapper,IHostEnvironment env)
        {
            _kshubService = kshubService;
            _mapper = mapper;
            this.env = env;
        }
        [HttpGet]
        [Route("Article")]
        public async ValueTask<ArticleDetailDto> GetArticleAsync(Guid articleId)
        {
            try
            {
                var article = await _kshubService.ArticleService.GetArticleAsync(articleId);
                return _mapper.Map<ArticleDetailDto>(article);
            }
            catch (Exception)
            {
                throw new _401Exception("It doesn't exist this article!");
            }
        }
        [HttpGet]
        [Route("AutherArticle")]
        public async ValueTask<List<ArticleDetailDto>> GetAutherArticleAsync(Guid autherId)
        {
            try
            {
                List<ArticleDetailDto> dtos = new List<ArticleDetailDto>();
                var articles = await _kshubService.ArticleService.GetAutherArticleAsync(autherId);
                foreach(var item in articles)
                {
                    dtos.Add(_mapper.Map<ArticleDetailDto>(item));
                }
                return dtos;
            }
            catch (Exception)
            {
                return null;
            }

        }

        [HttpPost]
        [Route("NewArticle")]
        public async ValueTask<ArticleDetailDto> AddNewArticle(AddArticleDto addArticleDto)
        {
            var authorId =Guid.Parse(User.Identity.Name);
            try
            {
                await _kshubService.ArticleService.CheckArticleName(authorId, addArticleDto.Title);
                var article = new Article
                {
                    Id = Guid.NewGuid(),
                    AuthorId = authorId,
                    Title = addArticleDto.Title,
                    Description = addArticleDto.Description,
                    Content = addArticleDto.Content
                };
                await _kshubService.ArticleService.AddArticleAsync(article);
                return _mapper.Map<ArticleDetailDto>(article);
            }
            catch (Exception)
            {
                throw new _401Exception($"The article with title: {addArticleDto.Title} exists!");
            }
        }
    }
}
