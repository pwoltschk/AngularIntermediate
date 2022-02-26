using Infrastructure;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddOpenApiDocument(config =>
    config.Title = "ProjectManager API");

var app = builder.Build();

// Initialise and seed the database
#if DEBUG
using (var scope = app.Services.CreateScope())
{
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        initializer.Initialise();
        initializer.Seed();
}
#endif

app.UseStaticFiles();

app.UseSwaggerUi3(configure =>
{
    configure.DocumentPath = "/api/v1/openapi.json";
});


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();


app.MapControllers();
app.Run();