using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoveCraft.Kshub.Dto;
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
        public async ValueTask AddKsAsymc(AddKsDto addKsDto)
        {

        }

    }
}
