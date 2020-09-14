using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class KsController : Controller
    {
        private KshubService _kshubService { get; }
        private IHostEnvironment _env { get; }
        private IMapper _mapper { get; }
        public KsController(KshubService kshubService, IHostEnvironment env, IMapper mapper)
        {
            _kshubService = kshubService;
            _mapper = mapper;
            _env = env;
        }

        [HttpPost]
        [Route("AddKs")]
        public async ValueTask AddKsAsync(AddKsDto addKsDto)
        {
            var ks = _mapper.Map<Ks>(addKsDto);
            var stuId = Guid.Parse(User.Identity.Name);
            ks.ProjectManager = stuId;
            ks.Id = Guid.NewGuid();
            var user = await _kshubService.KshubUserServices.FindUserAsync(stuId);
            ks.BelongCollegeId= user.BelongId;
            await _kshubService.KsServices.AddAsync(ks);
        }

        [HttpGet]
        public async ValueTask<IEnumerable<KsDetailDto>> GetKsDetailAsync
            (int page=0,int pagesize = 10,bool IsDescending=true)
        {
            return await _kshubService.KsServices.GetAsync(t => _mapper.Map<KsDetailDto>(t));
        }
    }
}
