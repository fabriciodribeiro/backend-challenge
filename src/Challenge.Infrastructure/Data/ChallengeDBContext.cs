using Challenge.Application.Interfaces;
using Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Data
{
    public class ChallengeDBContext : DbContext, IChallengeDBContext
    {       

        public ChallengeDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Trade> Traders { get; set; }
    }
}
