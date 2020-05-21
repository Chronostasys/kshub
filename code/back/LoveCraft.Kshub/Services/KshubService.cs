using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public KshubUserServices KshubUserServices { get; }
        public CourseServices CourseServices { get; }
        public UserInCourseService UserInCourseService { get; }
        public EmailService EmailService { get; }
        public ConcurrentDictionary<Guid, object> tokens { get; }
        public KshubService(IDatabaseSettings databaseSettings,IHostEnvironment env,IMapper mapper,EmailSender<Models.Email> email)
        {
            BsonSerializer.RegisterIdGenerator(typeof(Guid), GuidGenerator.Instance);
            try
            {
                EmailService = new EmailService(email);
                KshubUserServices = new KshubUserServices(databaseSettings);
                CourseServices = new CourseServices(databaseSettings);
                UserInCourseService = new UserInCourseService(databaseSettings);
                tokens = new ConcurrentDictionary<Guid, object>();
                this.env = env;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }
        IHostEnvironment env { get; }

    }
}
