using Microsoft.EntityFrameworkCore;
using PRN232.LMS.API.Configurations;
using PRN232.LMS.API.Formatters;
using PRN232.LMS.Repositories.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;

    options.OutputFormatters.Add(new CsvOutputFormatter());
    options.OutputFormatters.Add(new HtmlOutputFormatter());
})
.AddXmlSerializerFormatters()
.AddXmlDataContractSerializerFormatters();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "PRN232.LMS API", Version = "v1" });
    options.OperationFilter<LowercaseQueryParameterFilter>();
});

// Database
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddDependencyInjection();

var app = builder.Build();

// DB migrate + seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LmsdbContext>();

    await db.Database.MigrateAsync();
    DbSeeder.Seed(db);
}

// Middleware
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PRN232.LMS API v1");
    c.RoutePrefix = "swagger"; // serve UI at /swagger
});

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();