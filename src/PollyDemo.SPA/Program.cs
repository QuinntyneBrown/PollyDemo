using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;
using PollyDemo.Infrastructure.DelegatingHandlers;
using PollyDemo.SPA.Clients;
using System;
using System.Net.Http;

namespace PollyDemo.SPA
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<RetryHandler>();

            services
                .AddHttpClient<CompaniesClient>("companies",client =>
            {
                client.BaseAddress = new Uri("http://localhost:4861");
            }).AddHttpMessageHandler<RetryHandler>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddSpaStaticFiles(configuration 
                => configuration.RootPath = "ClientApp/dist");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles()
                .UseSpaStaticFiles();

            app.UseMvc();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                    spa.UseAngularCliServer(npmScript: "start");
            });
        }
    }
}




