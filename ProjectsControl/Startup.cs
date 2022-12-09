using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectsControl.Data;
using ProjectsControl.Models;
using Microsoft.AspNetCore.Authorization.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectsControl
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
            // Identity
            string IdentityEnvConnection = Environment.GetEnvironmentVariable("IdentityConnection");
            if (!string.IsNullOrEmpty(IdentityEnvConnection))
            {
                Console.WriteLine($" String Connection exist in Enviroment - {IdentityEnvConnection}");
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        IdentityEnvConnection
                    )
               );
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                         Configuration.GetConnectionString("DefaultConnection")
                    )
               );
                Console.WriteLine($"String connection in Configuration - {Configuration.GetConnectionString("DefaultConnection")}");
            }

            // Projects
            string DBDProjectsEnvConnection = Environment.GetEnvironmentVariable("DBProjectsConnection");
            if (!string.IsNullOrEmpty(DBDProjectsEnvConnection))
            {
                Console.WriteLine("String Connection exist in enviroment");
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString($"DefaultConnection - {DBDProjectsEnvConnection}")
                    )
                );
            }
            else
            {
                Console.WriteLine($"String Connection exist in configuration -{Configuration.GetConnectionString("DBProjectsConnection")}");
                services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(
                            Configuration.GetConnectionString("DBProjectsConnection")
                        )
                   );
            }


            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(
                options => options.SignIn.RequireConfirmedAccount = true
                ).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();



            services.AddControllersWithViews();

            /// DEPENDENCY INJECTION PROJECTS DATA BASE 
            string connString = ConfigurationExtensions.GetConnectionString(this.Configuration, "DBProjectsConnection");
            services.AddDbContext<DBProjectContext>(
                    option => option.UseSqlServer(connString)
                );
            services.AddMvc(options => options.EnableEndpointRouting = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                  name: "Administration",
                  areaName: "Admin",
                  pattern: "Admin/{controller=Manage}/{action=Index}/{id?}"
                ); ;
                endpoints.MapRazorPages();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                  name: "Administration",
                  areaName: "Admin",
                  pattern: "Admin/{controller=Manage}/{action=Index}/{id?}/{roleid?}/{Password1?}/{Password2?}"
                ); ;
                endpoints.MapRazorPages();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }
    }
}
