using Challenge.Application.Common.ViewModel;
using Challenge.Application.Interfaces.Services;
using Challenge.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Challenge.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<ITradeService, TradeService>();

            return services;
        }
    }
}
