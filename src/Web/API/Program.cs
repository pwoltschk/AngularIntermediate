
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddOpenApiDocument(config =>
    config.Title = "ProjectManager API");

var app = builder.Build();

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