using System;
using System.Text;
using AffinRuService;
using AutoMapper;
using GoodNews.API.Filters;
using GoodNews.Core;
using GoodNews.Data;
using GoodNews.Data.Entities;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ParserService;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace GoodNews.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\GoodNews.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

           

            string connection = Configuration.GetConnectionString("AzureConnection");
            services.AddDbContext<GoodNewsContext>(options => options.UseSqlServer(
                connection, x => x.MigrationsAssembly("GoodNews.Migrations")));
            services.AddAutoMapper(typeof(Startup));

            services.AddMediatR(AppDomain.CurrentDomain.Load("GoodNews.MediatR"));
            services.AddTransient<IMediator, Mediator>();
            services.AddTransient<IParser, NewsParser>();
            services.AddTransient<INewsService, NewsService.NewsService>();
            services.AddTransient<ILemmatization, LemmatizationService.LemmatizationService>();
            services.AddTransient<IAffinService, AffinRuDictionary>();
            services.AddTransient<IRatingService, SentimentRatingService.SentimentRatingService>();
            services.AddCors();

            services.AddHangfire(config => config.UseSqlServerStorage(
                        Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info()
                {
                    Title = "GoodNews API",
                    Version = "v1.0"
                });
            });

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;

                })
                .AddEntityFrameworkStores<GoodNewsContext>();

            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseSwagger();
            app.UseSwaggerUI(sw =>
            {
                sw.SwaggerEndpoint("/swagger/v1/swagger.json", "GoodNews API v1");
            });
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseHangfireServer();

            app.UseHangfireDashboard("/admin/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });


            var newsService = app.ApplicationServices.GetService<INewsService>();
            RecurringJob.AddOrUpdate(() => newsService.Start(),
                Cron.Daily);
        }
    }
}
