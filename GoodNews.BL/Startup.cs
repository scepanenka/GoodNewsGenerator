using Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GoodNews.DAL;
using GoodNews.DAL.Repository;
using GoodNews.Data.Entities;
using Services;
using Services.Parsers;

namespace GoodNews.BL
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<GoodNewsContext>(options => options.UseSqlServer(connection, x => x.MigrationsAssembly("GoodNews.DAL")));
            services.AddTransient<IRepository<Article>, ArticleRepository>();
            services.AddTransient<IRepository<Source>, SourceRepository>();
            services.AddTransient<IRepository<Category>, CategoryRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IS13Parser, S13Parser>();
            services.AddTransient<IOnlinerParser, OnlinerParser>();
            services.AddTransient<ITutByParser, TutByParser>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=News}/{action=Index}/{id?}");
            });
        }
    }
}
