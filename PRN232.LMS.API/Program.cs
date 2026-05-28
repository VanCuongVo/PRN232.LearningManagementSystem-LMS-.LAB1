using PRN232.LMS.API.Configurations;
using PRN232.LMS.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Content Negotation
builder.Services.AddMvcConfiguration();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

// Database
builder.Services.AddDatabase(builder.Configuration);
// Service
builder.Services.AddDependencyInjection();
// Validator
builder.Services.AddFluentValidationConfig();
builder.Services.AddValidationConfiguration();
builder.Services.AddCustomJsonOptions();

var app = builder.Build();
// Middleware
// DB migrate + seed
// await app.InitialiseDatabaseAsync();
app.UseSwaggerConfiguration();
app.UseAuthentication();
app.UseAuthorization();
app.UseGlobalException();
app.MapControllers();
app.Run();