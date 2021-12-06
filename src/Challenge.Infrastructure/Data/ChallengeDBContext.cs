using Challenge.Application.Interfaces;
using Challenge.Core.Models;
using Challenge.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.Modified = DateTime.UtcNow;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TradeMap());
            builder.ApplyConfiguration(new PortfolioMap());
            builder.ApplyConfiguration(new AccountMap());
            
        }
    }
}
