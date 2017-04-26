using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NIP.Models;
using AutoMapper;
using NIP.Data;
using NIP.Tools;
using System.Reflection;

namespace NIP
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set;}
        public IConfigurationRoot Configuration { get; }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["DbContextSettings:ConnectionString"];
            
            services.AddDbContext<NIPDbContext>(opts => opts.UseSqlite(connectionString));
            services.AddMvc().AddControllersAsServices();  
            services.AddAutoMapper();       

            var builder = new ContainerBuilder();
            var assembly = typeof(NIPDbContext).GetTypeInfo().Assembly;            
                
            builder.Populate(services);
            builder.RegisterAssemblyTypes(assembly);
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();                       
            ApplicationContainer = builder.Build();
    
            return new AutofacServiceProvider(this.ApplicationContainer);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime, NIPDbContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(context);

            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
    }
}
