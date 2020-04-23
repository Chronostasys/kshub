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
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddControllers();
            IConfiguration config;
            config = Configuration.GetSection(nameof(MongoDbSettings));
            services.Configure<MongoDbSettings>(config);

            //������������е����лᱨ�����£�
            //Unable to resolve service for type 'LoveCraft.Kshub.Models.IDatabaseSettings' 
            //while attempting to activate 'LoveCraft.Kshub.Services.KshubService'.
            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            services.AddSingleton<KshubService>();
            services.AddTransient<IEmailSender, EmailSender>();


            services.AddAutoMapper(config=> {
                config.CreateMap<KshubUser, KshubUserDetailDto>();
                config.CreateMap<LogInDto, KshubUser>();
            },typeof(KshubUser),typeof(KshubUserDetailDto),typeof(LogInDto));
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
            
            app.UseHttpsRedirection();
            //����openApi�ķ���Swagger���ò���
            app.UseOpenApi(config =>
            {
                config.PostProcess = (doc, rec) =>
                {
                    doc.Schemes.Clear();
                    doc.Schemes.Add(NSwag.OpenApiSchema.Https);
                    rec.Scheme = "https";
                };
            });
            app.UseAuthentication();
            app.UseRouting();
            
            app.UseAuthorization();
            app.UseSwaggerUi3(confif=> { });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
