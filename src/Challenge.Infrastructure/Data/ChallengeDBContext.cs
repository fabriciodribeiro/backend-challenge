using Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Challenge.Infrastructure.Data
{
    public class ChallengeDBContext : DbContext
    {       

        public ChallengeDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Trade> Traders { get; set; }
    }
}
