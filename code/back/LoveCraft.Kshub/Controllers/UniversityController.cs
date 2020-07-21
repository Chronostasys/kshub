using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using LoveCraft.Kshub.Dto;
using LoveCraft.Kshub.Models;
using LoveCraft.Kshub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
    }
}
