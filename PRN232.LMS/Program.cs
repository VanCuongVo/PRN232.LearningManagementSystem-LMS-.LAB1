using Microsoft.EntityFrameworkCore;
using PRN232.LMS.Repositories.Data;
using PRN232.LMS.Repositories.IRepositories;
using PRN232.LMS.Repositories.Repositories;
using PRN232.LMS.Services.IServices;
using PRN232.LMS.Services.Services;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LmsdbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentRepositories, StudentRepositoies>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(
    typeof(IGenericRepositories<>),
    typeof(GenericRepositories<>)
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
