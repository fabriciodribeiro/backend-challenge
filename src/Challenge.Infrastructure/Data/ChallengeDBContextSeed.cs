using Challenge.Application.Services;
using Challenge.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Data
{
    public static class ChallengeDBContextSeed
    {
        public static async Task SeedSampleDataAsync(ChallengeDBContext context,
            ILogger logger)
        {
            try
            {
                // Seed the database
                if (!context.Accounts.Any())
                {
                    var salt = Guid.NewGuid().ToString().Replace("-", "");

                    var account = new Account
                    {
                        Id = Guid.NewGuid(),
                        Name = "my_first_name my_last_name",
                        UserName = "my_username",
                        Email = "my_email@email.com",
                        Created = DateTime.UtcNow,
                        Salt = salt,
                        Password = HashService.Cryptograph("123456", salt),
                        Portfolios = 
                        {
                            new Portfolio
                            {
                                Id = Guid.NewGuid(),
                                Name = "my_portfolio_1",
                                Created = DateTime.UtcNow,
                                User = new Account(),
                                Trades = {
                                    new Trade
                                    {
                                        Id = Guid.NewGuid(),
                                        Date = DateTime.UtcNow,
                                        Created = DateTime.UtcNow,
                                        Shares = 15,
                                        Price = 15.48m,
                                        Currency = "USD",
                                        MarketValue = 232.20m,
                                        Action = Core.Enums.Actions.buy,
                                        Note = null,
                                        Asset = "PT10Y"
                                    },
                                    new Trade
                                    {
                                        Id = Guid.NewGuid(),
                                        Date = DateTime.UtcNow,
                                        Created = DateTime.UtcNow,
                                        Shares = 15,
                                        Price = 15.48m,
                                        Currency = "USD",
                                        MarketValue = 232.20m,
                                        Action = Core.Enums.Actions.buy,
                                        Note = null,
                                        Asset = "PT20Y"
                                    }
                                }
                            }
                        }
                    };


                    context.Accounts.Add(account);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                throw;
            }
        }
    }
}
