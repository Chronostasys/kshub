using LoveCraft.Kshub.Controllers;
using LoveCraft.Kshub.Models;
using LoveCraft.Kshub.Services;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using LoveCraft.Kshub;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace KshubUnitTest
{
    public class UserControllerTest
    {
        public UserController UserController;
        public IMapper mapper;
        public IDatabaseSettings settings;
        public KshubService KshubService;
        public UserControllerTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile($"appsettings.json", false);
                })
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            KshubService = server.Services.GetService(typeof(KshubService)) as KshubService;
        }

        [Fact]
        public async Task Test1()
        {
            var re =await KshubService.KshubUserServices.FindUserAsync("AdminAccount");
            Assert.Equal("AfterUpdating",re.Name);
        }

        //这个单元测试好像有一点小问题，一会再改改
        [Fact]
        public void Add_Uni_Succ_Test()
        {
            var uni = new University
            {
                Id = Guid.NewGuid(),
                Name = "xxx",
                Desciption = "xxxx"
            };
             KshubService.UniversityServices.AddUniWithCheckAsync(uni);
            
        }
        [Fact]
        public async Task GetUniFailed_Test()
        {
            var id = Guid.NewGuid();
            University university=new University();
            try
            {
                university = await KshubService.UniversityServices.GetUniversityAsync(id);
            }
            catch { }
            Assert.Equal(id, university.Id);
        }
    }
}
