using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using LoveCraft.Kshub.Dto;
using LoveCraft.Kshub.Models;
using LoveCraft.Kshub.Services;
using Lucene.Net.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace LoveCraft.Kshub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : Controller
    {
        private KshubService _kshubService { get; }
        private IHostEnvironment _env { get; }
        private IMapper _mapper { get; }
        public UniversityController(KshubService kshubService, IHostEnvironment env, IMapper mapper)
        {
            _kshubService = kshubService;
            _mapper = mapper;
            _env = env;
        }
        
        [HttpPost]
        [Route("AddUni")]
        [Authorize(Roles =KshubRoles.Admin)]
        public async ValueTask<UniDetailDto> AddUniAsync(AddUnilDto addUnilDto)
        {
            var uni = _mapper.Map<University>(addUnilDto);
            uni.Id = Guid.NewGuid();
            return _mapper.Map<UniDetailDto>(await _kshubService.UniversityServices.AddUniWithCheckAsync(uni));
        }

        [HttpGet("GetUniversity/{id}")]
        public async ValueTask<UniDetailDto> GetUniAsync(Guid id)
        {
            var r = await _kshubService.UniversityServices.GetUniversityAsync(id);
            return _mapper.Map<UniDetailDto>(r);     
        }

        [HttpGet]
        [Route("GetUniversities")]
        public async ValueTask<IEnumerable<UniDetailDto>> GetUniversitysAsync(int page=0,int pagesize=10,
            bool isDescending=true)
        {
            var projection = Builders<University>.Projection.Expression(t => _mapper.Map<UniDetailDto>(t));
            SortDefinition<University> sortfilter;
            var sort = Builders<University>.Sort;
            if (isDescending)
            {
                sortfilter= sort.Descending(t => t.CreateTime);

            }
            else
            {
                sortfilter = sort.Ascending(t => t.CreateTime);
            }
            
            var results =await _kshubService.UniversityServices.GetAsync(projection, page, pagesize,sortfilter,Builders<University>.Filter.Empty);

            return results;
        }
    }
}
