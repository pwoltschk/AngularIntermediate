using Domain.Entities;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Extensions;
using Duende.IdentityServer.EntityFramework.Interfaces;
using Duende.IdentityServer.EntityFramework.Options;
using Infrastructure.Data.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Infrastructure.Data;

public class ApplicationDbContext(
    DbContextOptions options,
    AuditableEntityInterceptor auditableEntityInterceptor,
    DomainEventsDispatcherInterceptor dispatcherInterceptor,
    IOptions<OperationalStoreOptions> operationalStoreOptions)
    : IdentityDbContext<IdentityUser, IdentityRole, string>(options), IPersistedGrantDbContext
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<WorkItem> WorkItems => Set<WorkItem>();
    public DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;
    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; } = null!;
    public DbSet<Key> Keys { get; set; } = null!;

    Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .AddInterceptors(auditableEntityInterceptor);
        optionsBuilder
            .AddInterceptors(dispatcherInterceptor);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly());

        builder.ConfigurePersistedGrantContext(operationalStoreOptions.Value);
        base.OnModelCreating(builder);
    }
}