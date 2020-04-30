﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        public KshubService(IDatabaseSettings databaseSettings,IHostEnvironment env,IMapper mapper)
        {
            BsonSerializer.RegisterIdGenerator(typeof(Guid), GuidGenerator.Instance);
            try
            {
                KshubUserServices = new KshubUserServices(databaseSettings);
                CourseServices = new CourseServices(databaseSettings);
                UserInCourseService = new UserInCourseService(databaseSettings);
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