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
            services.AddDbContext<WozUContext>(options => options.UseInMemoryDatabase("WozUContext"));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Call database initialization method
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

        // Function to fill database with seed data
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<WozUContext>();
            if (!context.Customers.Any())
            {
                context.Customers.Add(new Customer { FirstName = "Frodo", LastName = "Baggins", Email = "frodo@theShire.net" });
                context.Customers.Add(new Customer { FirstName = "Steve", LastName = "Bishop", Email = "steve.bishop@woz-u.com" });
                context.SaveChanges();
            }
        }
    }
}
