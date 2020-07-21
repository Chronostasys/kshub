using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using LimFx.Business.Models;
using LimFx.Business.Services;
using LoveCraft.Kshub.Models;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
namespace LoveCraft.Kshub.Services
{
    public class KshubService
    {
        public UserServices KshubUserServices { get; }
        public EmailService EmailService { get; }
        public ConcurrentDictionary<Guid, object> tokens { get; }
        public LoadFileServices LoadFileServices { get; }

        public UniversityServices UniversityServices { get; }
        public KshubService(IDatabaseSettings databaseSettings,IHostEnvironment env,IMapper mapper,EmailSender<Models.Email> email)
        {
            BsonSerializer.RegisterIdGenerator(typeof(Guid), GuidGenerator.Instance);
            try
            {
                LoadFileServices = new LoadFileServices(databaseSettings);
                EmailService = new EmailService(email);
                KshubUserServices = new UserServices(databaseSettings);
                UniversityServices = new UniversityServices(databaseSettings);
                tokens = new ConcurrentDictionary<Guid, object>();
                this.env = env;
            }
            catch (Exception e)
            {

            }
           
        }
        IHostEnvironment env { get; }

    }
}
