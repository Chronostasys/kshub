using AutoMapper;
using LimFx.Business.Exceptions;
using LoveCraft.Kshub.Dto;
using LoveCraft.Kshub.Models;
using LoveCraft.Kshub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollegeController:Controller
    {
        private readonly KshubService _kshubService;
        readonly IMapper _mapper;
        IHostEnvironment env;
        public CollegeController(KshubService kshubService, IMapper mapper, IHostEnvironment env)
        {
            _kshubService = kshubService;
            _mapper = mapper;
            this.env = env;
        }
        [HttpPost("AddCollege")]
        public async ValueTask AddCollegeAsync(AddCollegeDto addCollegeDto)
        {
            var c = _mapper.Map<College>(addCollegeDto);
            c.Id = Guid.NewGuid();
            await _kshubService.CollegeServices.AddCollegeAsync(c);
        }

        [HttpGet("GetCollege/{name}")]
        
        public async ValueTask<CollegeDetailDto> GetCollegeByNameAsync(string name)
        {
            try
            {
                var filter = Builders<College>.Filter.Eq(t => t.Name, name);
                //t => _mapper.Map<CollegeDetailDto>(t)
                var r = await _kshubService.CollegeServices.GetAsync(t => _mapper.Map<CollegeDetailDto>(t),0,1,null,true,filter);
                return r.First();
            }
            catch(Exception e)
            {
                throw new _401Exception(e.Message);
            }

        }

        [HttpGet("GetColleges")]
        public async ValueTask<IEnumerable<CollegeDetailDto>> GetUniversitysAsync(int page = 0, int pagesize = 10,
            bool isDescending = true)
        {
            var projection = Builders<College>.Projection.Expression(t => _mapper.Map<CollegeDetailDto>(t));
            SortDefinition<College> sortfilter;
            var sort = Builders<College>.Sort;
            if (isDescending)
            {
                sortfilter = sort.Descending(t => t.CreateTime);

            }
            else
            {
                sortfilter = sort.Ascending(t => t.CreateTime);
            }

            var results = await _kshubService.CollegeServices.GetAsync(projection, page, pagesize, sortfilter, Builders<College>.Filter.Empty);

            return results;
        }

    }
}
