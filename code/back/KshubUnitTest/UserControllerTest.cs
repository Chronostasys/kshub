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
    }
}
