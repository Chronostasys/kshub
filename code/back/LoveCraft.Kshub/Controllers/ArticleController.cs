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
    }
}
