﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveCraft.Kshub.Models;
using LoveCraft.Kshub.Services;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using OpenXmlPowerTools;
using LoveCraft.Kshub.Dto;
using System.IO;
using Microsoft.AspNetCore.Http;
using LimFx.Business.Exceptions;

namespace LoveCraft.Kshub.Controllers
{
    public class FakeController:Controller
    {
        private KshubService _kshubService { get;}
        private IHostEnvironment _env { get; }
        private IMapper _mapper { get; }
        public FakeController(KshubService kshubService,IHostEnvironment env,IMapper mapper)
        {
            _kshubService = kshubService;
            _env = env;
            _mapper = mapper;
        }

        [HttpGet("dropuser")]
        public async ValueTask DropUser()
        {
            await _kshubService.KshubUserServices.collection.Database.DropCollectionAsync(_kshubService.KshubUserServices.collection.CollectionNamespace.CollectionName);
        }

        [HttpPost]
        [Route("AddFakeUser")]
        public async ValueTask AddFakeUser()
        {
            for (int i = 0; i < 20; i++)
            {
                await _kshubService.KshubUserServices.AddUserWithCheckAsync(
                    new KshubUser
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test" + i.ToString(),
                        UserId = (100 + i).ToString(),
                        Email = (100 + i).ToString() + "@test.com",
                        PassWordHash = "12345678a",
                        IsEmailConfirmed=true,
                        Roles = new List<string> { "User" },
                    }); 
            }
        }
        [HttpPost]
        [Route("ForTesting")]
        public async ValueTask AddCollegeAsync()
        {
            University university = new University
            {
                Id = Guid.NewGuid(),
                Name = "PlantTreesUniversity",
                Desciption = "We love planting trees:)",

            };
            College college = new College
            {
                Id = Guid.NewGuid(),
                Name = "CSE",
                BelongUniId = university.Id,

            };
            KshubUser collegeAdmin = new KshubUser
            {
                Name = "CSECollegeAdmin",
                UserId = "CSECollegeAdmin",
                CollegeId = college.Id,
                Roles = new List<string> { KshubRoles.CollegeAdmin, KshubRoles.User },
                Id = Guid.NewGuid(),
                Email = "test1@kshub.com",
                PassWordHash = "Helloworld2020@"
            };
            await _kshubService.CollegeServices.AddCollegeWithCheckingAsync(college);
            await _kshubService.KshubUserServices.AddUserWithCheckAsync(collegeAdmin);
            await _kshubService.UniversityServices.AddUniWithCheckAsync(university);

        }


    }
}
