using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly KshubService _kshubService;
        readonly IMapper _mapper;
        IHostEnvironment env;

    }
}
