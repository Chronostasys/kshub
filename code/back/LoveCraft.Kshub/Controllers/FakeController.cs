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
        IDatabaseSettings _settings {get;}
        public FakeController(KshubService kshubService,IHostEnvironment env,
            IMapper mapper, IDatabaseSettings settings)
        {
            _settings = settings;
            _kshubService = kshubService;
            _env = env;
            _mapper = mapper;
        }

        [HttpGet("DropAndFake")]
        public async ValueTask DropAndFake()
        {
            await DropAll();
            await FakeAll();
        }

        [HttpGet("fakeall")]
        public async ValueTask FakeAll()
        {
            var uniCtr = new UniversityController(_kshubService,_env,_mapper);
            CollegeController collCtr = new CollegeController(_kshubService,_mapper,_env);
            var ksCtr= new KsController(_kshubService,_env,_mapper);
            var cCtr = new CourseController(_kshubService,_mapper,_env);
            var de = await uniCtr.AddUniAsync(new AddUniDto
            {
                Name="周犬是沙雕大学",
                Desciption="周犬是一种神奇的犬类生物，是一种上古神兽",
                CoverUrl="https://th.bing.com/th/id/OIP.zHNUC0rIIK61Kx6lxDqT5gHaEK?w=285&h=180&c=7&o=5&dpr=2&pid=1.7"
            });
            var college = await collCtr.AddCollegeAsync(new AddCollegeDto
            {
                Name = "生艹学院",
                Description = "wowowowowowowowowwwww",
                BelongUniId = de.Id,
                CoverUrl="https://th.bing.com/th/id/OIP.zHNUC0rIIK61Kx6lxDqT5gHaEK?w=285&h=180&c=7&o=5&dpr=2&pid=1.7"
            });
            List<Guid> uids = new List<Guid>();
            await foreach (var item in AddFakeUser(college.Id))
            {
                uids.Add(item.Id);
            }
            var admin = await AddCollegeAdminAsync(college.Id);
            var cid = await cCtr.AddCourseAsync(new AddCourseDto
            {
                Name = "生艹课",
                TeacherIds = uids,
                Description = "wowowowowowowowowwwww",
                CoverUrl="https://th.bing.com/th/id/OIP.zHNUC0rIIK61Kx6lxDqT5gHaEK?w=285&h=180&c=7&o=5&dpr=2&pid=1.7"
            },admin.Id);
            
            var ks = _mapper.Map<Ks>(new AddKsDto
            {
                Name = "生艹课设",
                Description = "wowowowowowowowowwwww",
                Abstract = "如何高效生艹",
                Participants = uids,
                CoverUrl = "https://th.bing.com/th/id/OIP.zHNUC0rIIK61Kx6lxDqT5gHaEK?w=285&h=180&c=7&o=5&dpr=2&pid=1.7",
                ProjectUrl = "https://www.limfx.pro",
                Keywords = new List<string>{"wow","Wow","WoW"},
                CourseId = cid
            });
            var managerId = admin.Id;
            ks.ProjectManager = managerId;
            ks.Id = Guid.NewGuid();
            ks.CollegeId= admin.CollegeId;

            await _kshubService.KsServices.AddAsync(ks);
        }
        [HttpGet("dropall")]
        public async ValueTask DropAll()
        {
            var type = _settings.GetType();
            var props = type.GetProperties();
            var db = _kshubService.KshubUserServices.collection.Database;
            foreach (var item in props)
            {
                try
                {
                    await db.DropCollectionAsync(item.GetValue(_settings) as string);
                }
                catch (System.Exception)
                {
                }
                
            }
        }

        [HttpGet("dropuser")]
        public async ValueTask DropUser()
        {
            await _kshubService.KshubUserServices.collection.Database.DropCollectionAsync(_kshubService.KshubUserServices.collection.CollectionNamespace.CollectionName);
        }

        [HttpPost]
        [Route("AddFakeUser")]
        public async IAsyncEnumerable<KshubUser> AddFakeUser(Guid collegeId)
        {
            for (int i = 0; i < 20; i++)
            {
                yield return await _kshubService.KshubUserServices.AddUserWithCheckAsync(
                    new KshubUser
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test" + i.ToString(),
                        UserId = (100 + i).ToString(),
                        Email = (100 + i).ToString() + "@test.com",
                        PassWordHash = "12345678a",
                        IsEmailConfirmed=true,
                        Roles = new List<string> { "User", KshubRoles.Teacher },
                        CollegeId = collegeId
                    }); 
            }
        }
        [HttpPost]
        [Route("ForTesting")]
        public async ValueTask<KshubUser> AddCollegeAdminAsync(Guid collegeId)
        {
            KshubUser collegeAdmin = new KshubUser
            {
                Name = "CSECollegeAdmin",
                UserId = "CSECollegeAdmin",
                CollegeId = collegeId,
                Roles = new List<string> { KshubRoles.CollegeAdmin, KshubRoles.User },
                Id = Guid.NewGuid(),
                Email = "test1@kshub.com",
                PassWordHash = "Helloworld2020@"
            };
            await _kshubService.KshubUserServices.AddUserWithCheckAsync(collegeAdmin);
            await _kshubService.KshubUserServices.SignInWithoutCheckAsync(HttpContext,
                collegeAdmin, true);
            return collegeAdmin;

        }


    }
}
