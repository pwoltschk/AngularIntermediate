using Application.Common.Behaviours;
using Application.WorkItems.Events;
using Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        services.AddTransient<INotificationHandler<WorkItemAssignedDomainEvent>, WorkItemAssignedDomainEventHandler>();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .CreateLogger();

        services.AddSingleton(Log.Logger);

        return services;
    }
}