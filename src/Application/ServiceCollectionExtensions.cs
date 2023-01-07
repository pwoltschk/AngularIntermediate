using Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console()
                .Enrich
                .FromLogContext()
                .MinimumLevel
                .Information()
                .CreateLogger();

            services.AddSingleton(Log.Logger);

            return services;
        }
    }
}
