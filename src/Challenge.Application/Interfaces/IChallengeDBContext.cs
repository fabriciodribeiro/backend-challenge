using Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Application.Interfaces
{
    public interface IChallengeDBContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Portfolio> Portfolios { get; set; }
        DbSet<Trade> Traders { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
