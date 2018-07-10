using Microsoft.EntityFrameworkCore;
using PollyDemo.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PollyDemo.Core.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Company> Companies { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
