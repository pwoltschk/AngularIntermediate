using ApiServer;
using ApiServer.Identity;
using Application;
using Application.Common.Services;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using NSwag;
using NSwag.Generation.Processors.Security;
using Shared.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddApiServer()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "ProjectManager API";
    configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Type into the textbox: Bearer {your JWT token}."
    });

    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});


builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await initializer.InitialiseAsync();
    await initializer.SeedAsync();
}




app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseSwaggerUi(configure => configure.Path = "/api/v1/openapi.json");



app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();