using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LoveCraft.Kshub.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using LoveCraft.Kshub.Dto;
using LoveCraft.Kshub.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using LimFx.Business.Services;
using Microsoft.AspNetCore.Identity;
using LimFx.Business.Extensions;
using OpenXmlPowerTools;

namespace LoveCraft.Kshub
{

    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact { };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "No lisence, all rights reserved",
                        Url = string.Empty
                    };
                };
            });
            IConfiguration config;
            config = Configuration.GetSection(nameof(MongoDbSettings));
            services.Configure<MongoDbSettings>(config);
            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddSingleton<KshubService>();
            //services.AddSecrertRecord<SecretRecord>(o =>
            //    o.GrillenPassword = Configuration["GrillenPassword"]
            //);
            services.AddEmailSenderService<Email>(op =>
            {
                op.DatabaseName = "KshubDb";
                op.ConnectionString = "mongodb://localhost:27017";
                op.EmailCollectionName = "email";
                op.Interval = 5000;
                op.MaxEmailThreads = 5;
                op.SenderName = "Sample";
                op.SmtpHost = "email-smtp.ap-south-1.amazonaws.com";
                op.SmtpSender="AKIAV5CPNJ6423VOJYOR";
                op.SmtpPassword = "BKvwJYiv8SLtc+j2Cf4VW4j/0RSAV3T0td8XB2DSGqC9"; //Configuration["smtppassword"];
                op.TemplateDir = "Index.cshtml";
            });

            //������IEnumerable�᲻����һ��С���⣿
            //��û������typeof
            services.AddAutoMapper(config=> {
                config.CreateMap<KshubUser, UserDetailDto>();
                config.CreateMap<AddUserDto, KshubUser>();
                config.CreateMap<LogInDto, KshubUser>();
                config.CreateMap<AddUniDto, University>();
                config.CreateMap<University, UniDetailDto>();
                config.CreateMap<College, CollegeDetailDto>();
                config.CreateMap<AddCollegeDto, College>();
                config.CreateMap<AddCourseDto, Course>();
                config.CreateMap<Course, CourseDetailDto>();
                config.CreateMap<AddClassDto, Class>();
                config.CreateMap<AddKsDto, Ks>();
                config.CreateMap<Ks, KsDetailDto>();
            }, typeof(KshubUser), typeof(UserDetailDto), typeof(LogInDto)
           ,typeof(AddUserDto)
            ,typeof(AddUniDto),typeof(University), typeof(UniDetailDto)
            ,typeof(College),typeof(CollegeDetailDto),typeof(AddCollegeDto)
            ,typeof(Course), typeof(CourseDetailDto),typeof(AddCourseDto)
            ,typeof(Class),typeof(AddClassDto)
            ,typeof(AddKsDto),typeof(KsDetailDto),typeof(Ks)
            );
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(op =>
                {
                    op.Events.OnRedirectToAccessDenied += (o) => throw new Exception("UnAuthorized!");
                });
            services.AddAuthorization();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { 
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseLimFxExceptionHandler();
            //�����ṩ��̬�ļ����м��
            app.UseStaticFiles();
            
            //app.UseHttpsRedirection();
            //����openApi�ķ���Swagger���ò���
            app.UseOpenApi(config =>
            {
                //config.PostProcess = (doc, rec) =>
                //{
                //    doc.Schemes.Clear();
                //    doc.Schemes.Add(NSwag.OpenApiSchema.Https);
                //    rec.Scheme = "https";
                //};
            });
            app.UseAuthentication();
            app.UseRouting();
            
            app.UseAuthorization();
            app.UseSwaggerUi3(confif=> { });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
