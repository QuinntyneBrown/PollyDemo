using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using PollyDemo.SPA.Clients;
using System;

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
            services.AddHttpClient<CompaniesClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:4861");
            }).
            AddTransientHttpErrorPolicy(policyBuilder
                        => policyBuilder.CircuitBreakerAsync(
                            handledEventsAllowedBeforeBreaking: 2,
                            durationOfBreak: TimeSpan.FromMinutes(1)));

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




