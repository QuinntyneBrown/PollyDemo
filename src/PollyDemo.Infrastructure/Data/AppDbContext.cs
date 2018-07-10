using Microsoft.EntityFrameworkCore;
using PollyDemo.Core.Interfaces;
using PollyDemo.Core.Models;

namespace PollyDemo.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options)
            :base(options) {
        }    
        
        public DbSet<Company> Companies { get; set; }
    }
}
