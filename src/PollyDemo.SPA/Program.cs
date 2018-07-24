using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
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

            services.AddHttpContextAccessor();

            // https://youtu.be/Lb12ZtlyMPg?t=2687

            // Using HttpClientFactory under the hood
            // https://github.com/aspnet/HttpClientFactory/blob/master/src/Microsoft.Extensions.Http/DependencyInjection/HttpClientBuilderExtensions.cs

            //services.AddTransient<CompaniesClient>(s =>
            //{
            //    var httpClientFactory = s.GetRequiredService<IHttpClientFactory>();
            //    var httpClient = httpClientFactory.CreateClient("companies");

            //    var typedClientFactory = s.GetRequiredService<ITypedHttpClientFactory<CompaniesClient>>();
            //    return typedClientFactory.CreateClient(httpClient);
            //});

            // Caching the HttpClient can result into issues
            // Framework always gives a new HttpClient, but the message handlers and connections are cached appropiately

            // DNS issues

            services
                .AddHttpClient<CompaniesClient>("companies",client =>
            {
                client.BaseAddress = new Uri("http://localhost:61448");
            })
            .SetHandlerLifetime(TimeSpan.FromSeconds(2))
            .AddHttpMessageHandler<RetryHandler>();
            
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




