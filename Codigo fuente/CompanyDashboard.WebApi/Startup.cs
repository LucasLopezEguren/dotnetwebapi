using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyDashboard.BusinessLogic;
using CompanyDashboard.BusinessLogic.Interface;
using CompanyDashboard.DataAccess;
using CompanyDashboard.DataAccess.Interface;
using CompanyDashboard.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CompanyDashboard.WebApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<CompanyDashboardContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //aca van las inicializaciones de las logicas con las Ilogicas "services.AddScoped<Ihomework, homework>();
            services.AddScoped<DbContext>(sp => sp.GetService<CompanyDashboardContext>());
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<ISessionLogic, SessionLogic>();
            services.AddScoped<IAreaLogic, AreaLogic>();
            services.AddScoped<IAreaUserLogic, AreaUserLogic>();
            services.AddScoped<IIndicatorLogic, IndicatorLogic>();
            services.AddScoped<INode, NodeLogic>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Area>, AreaRepository>();
            services.AddScoped<IRepository<Session>, SessionRepository>();
            services.AddScoped<IRepository<Indicator>, IndicatorRepository>();
            services.AddScoped<IRepository<AreaUser>, AreaUserRepository>();
            services.AddScoped<IRepository<UserIndicator>, UserIndicatorRepository>();


            // services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
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

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Default",
                    template: "api/{controller}/{id?}"
                );
            });
        }
    }
}
