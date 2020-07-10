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

namespace LoveCraft.Kshub
{

    public class Startup
    {

        public string password=null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            password = Configuration["GrillenPassword"];
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
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddControllers();
            IConfiguration config;
            config = Configuration.GetSection(nameof(MongoDbSettings));
            services.Configure<MongoDbSettings>(config);
            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddSingleton<KshubService>();
            services.AddSecrertRecord<SecretRecord>(o =>
                o.GrillenPassword = Configuration["GrillenPassword"]
            );
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

            //添加了IEnumerable会不会有一点小问题？
            //并没有添加typeof
            services.AddAutoMapper(config=> {
                config.CreateMap<KshubUser, UserDetailDto>();
                config.CreateMap<LogInDto, KshubUser>();
                config.CreateMap<Course, CourseDetailDto>();
                config.CreateMap<Article, ArticleDetailDto>();
                config.CreateMap<IEnumerable<Article>, IEnumerable<ArticleDetailDto>>();
            }, typeof(KshubUser), typeof(UserDetailDto), typeof(LogInDto), typeof(Course)
            , typeof(CourseDetailDto),typeof(Article),typeof(ArticleDetailDto),typeof(IEnumerable<Article>)
            ,typeof(IEnumerable<ArticleDetailDto>));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(op =>
                {
                    op.Events.OnRedirectToAccessDenied += (o) => throw new Exception("UnAuthorized!");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //配置提供静态文件的中间件
            app.UseStaticFiles();
            
            app.UseHttpsRedirection();
            //不加openApi的服务Swagger就用不了
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
