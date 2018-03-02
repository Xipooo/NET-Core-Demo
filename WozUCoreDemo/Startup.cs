using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WozUCoreDemo.Models;

namespace WozUCoreDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WozUContext>(
                // Use MySql package for connection (may require restart of VSCode)
                options => options.UseMySql(
                    // Use appsettings.json ConnectionStrings: DefaultConnection
                    // NOTE: You should add appsettings.json to .gitIgnore to prevent 
                    //      settings from being visible in public repositories
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Initialize(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(r =>
                r.MapRoute(
                    "default",
                    "{controller=HomePage}/{action=Index}/{id?}"
                )
            );
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            //Add check for pending migrations to database
            //Try/Catch for catching when migration created causes error
            try
            {
                var context = serviceProvider.GetService<WozUContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Customers.Any())
                {
                    context.Customers.Add(new Customer { FirstName = "Frodo", LastName = "Baggins", Email = "frodo@theShire.net" });
                    context.Customers.Add(new Customer { FirstName = "Steve", LastName = "Bishop", Email = "steve.bishop@woz-u.com" });
                    context.SaveChanges();
                }
            }
            catch (Exception ex){
                Console.WriteLine("Unable to seed database.");
                Console.WriteLine(ex.Message);
            }


        }
    }
}
