using PollyDemo.Core.Models;
using PollyDemo.Infrastructure.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace PollyDemo.API
{
    public class SeedData
    {
        public static void Seed(AppDbContext context)
        {
            CompnyConfiguration.Seed(context);

            context.SaveChanges();
        }

        internal class CompnyConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                if (context.Companies.FirstOrDefault(x => x.Name == "Nike") == null)
                {
                    var nike = new Company()
                    {
                        Name = "Nike"
                    };

                    var ralph = new Company()
                    {
                        Name = "Ralph Lauren"
                    };

                    var kate = new Company()
                    {
                        Name = "Kate Spade"
                    };


                    context.Companies.AddRange(new[] { nike, ralph, kate });
                }

                context.SaveChanges();
            }
        }


    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly)
                .Build();

            return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"])
                .Options);
        }
    }
}
