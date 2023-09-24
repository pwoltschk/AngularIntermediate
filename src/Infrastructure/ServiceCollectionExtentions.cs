using Application.Common.Services;
using Domain.Entities;
using Domain.Primitives;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Emails;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<AuditableEntityInterceptor>();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddScoped<ApplicationDbContextInitialiser>();


        services.AddIdentityServer()
            .AddApiAuthorization<IdentityUser, ApplicationDbContext>()
            .AddProfileService<CustomProfileService>();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IRepository<Project>, ProjectRepository>();
        services.AddScoped<IRepository<WorkItem>, WorkItemRepository>();
    }
}