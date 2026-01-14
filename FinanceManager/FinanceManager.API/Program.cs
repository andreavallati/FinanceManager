using FinanceManager.API.Application.Mapping;
using FinanceManager.API.Extensions;
using FinanceManager.API.Infrastructure.Context;
using FinanceManager.API.Infrastructure.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration
builder.InjectConfig();
builder.InjectConfigSections();

// Authentication & Authorization
builder.ConfigureAuthentication();
builder.ConfigureAuthorization();

// Validator
builder.Services.InjectValidators();

// Services & Repositories
builder.Services.InjectServices();
builder.Services.InjectRepositories();

// Add Entity Framework Core and configure the SQL Server connection string
// Use Add-Migration InitialCreate and then Update-Database to let EF generate the DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(FinanceManagerProfile));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();

// Register Middlewares
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

await app.RunAsync();