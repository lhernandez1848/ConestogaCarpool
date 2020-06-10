using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConestogaCarpool.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ConestogaCarpool.Models;
using ConestogaCarpool.Repositories;
using ConestogaCarpool.BusinessLogic;

namespace ConestogaCarpool
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

            services.AddDbContext<ConestogaCarpoolContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ConestogaCarpoolConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //project wide dependecy Injections
            services.AddTransient<IVehicleLogic, VehicleLogic>();
            services.AddTransient<IRideLogic, RideLogic>();
            services.AddTransient<IReviewLogic, ReviewLogic>();
            services.AddTransient<IRequestLogic, RequestLogic>();
            services.AddTransient<IPostLogic, PostLogic>();
            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<IDriverLogic, DriverLogic>();

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IRideRepository, RideRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();

            //Add sessions and TempData services
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=User}/{action=SignIn}/{id?}");
            });
        }
    }
}
