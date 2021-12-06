using Challenge.Application.Interfaces;
using Challenge.Application.Interfaces.Repositories;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ChallengeDBContext>(options =>
                    options.UseInMemoryDatabase("CHALLENGEDB"));
            }
            else
            {
                services.AddDbContext<ChallengeDBContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("SQLSERVER")));
            }

            services.AddScoped<IChallengeDBContext>(provider => provider.GetService<ChallengeDBContext>());

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IPortfolioRepository, PortfolioRepository>();
            services.AddTransient<ITradeRepository, TradeRepository>();

            return services;
        }
    }
}
