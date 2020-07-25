using AutoMapper;
using DocumentFormat.OpenXml.Presentation;
using LoveCraft.Kshub.Dto;
using LoveCraft.Kshub.Models;
using LoveCraft.Kshub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCraft.Kshub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController:Controller
    {
        private readonly KshubService _kshubService;
        readonly IMapper _mapper;
        IHostEnvironment env;
        public ClassController(KshubService kshubService, IMapper mapper, IHostEnvironment env)
        {
            _kshubService = kshubService;
            _mapper = mapper;
            this.env = env;
        }

        [HttpPost]
        [Route("AddClass")]
        public async ValueTask AddClassAsync(AddClassDto addClassDto)
        {
            var c = _mapper.Map<Class>(addClassDto);
            c.Id = Guid.NewGuid();
            await _kshubService.ClassServices.AddAsync(c);
        }
    }
}
