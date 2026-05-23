using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PRN232.LMS.API.Configurations;
using PRN232.LMS.Repositories;
using PRN232.LMS.Repositories.Data;
using PRN232.LMS.Repositories.IRepositories;
using PRN232.LMS.Repositories.Repositories;
using PRN232.LMS.Services;
using PRN232.LMS.Services.IServices;
using PRN232.LMS.Services.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddDependencyInjection();

builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<LowercaseQueryParameterFilter>();
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LmsdbContext>();

    db.Database.Migrate();
    DbSeeder.Seed(db);
}
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();