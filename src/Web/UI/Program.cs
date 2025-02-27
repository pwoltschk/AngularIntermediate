using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Plk.Blazor.DragDrop;
using Shared.Identity;
using UI;
using UI.Identity;
using UI.Validators;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

AddApiClient<IProjectClient, ProjectClient>(builder);
AddApiClient<IWorkItemClient, WorkItemClient>(builder);
AddApiClient<IRolesClient, RolesClient>(builder);
AddApiClient<IUsersClient, UsersClient>(builder);

builder.Services
    .AddApiAuthorization()
    .AddAccountClaimsPrincipalFactory<PermissionAccountClaimsPrincipalFactory>();


builder.Services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
builder.Services.AddTransient<IValidator<WorkItemDto>, WorkItemDtoValidator>();
builder.Services.AddTransient<IValidator<ProjectDto>, ProjectDtoValidator>();


builder.Services.AddBlazorDragDrop();



await builder.Build().RunAsync();


static void AddApiClient<TClient, TImplementation>(WebAssemblyHostBuilder builder)
    where TClient : class
    where TImplementation : class, TClient

{
    builder.Services.AddHttpClient<TClient, TImplementation>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
        .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
}
