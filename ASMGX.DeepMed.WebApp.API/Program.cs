using ASMGX.DeepMed.Application;
using ASMGX.DeepMed.Business;
using ASMGX.DeepMed.Infrastructure;
using ASMGX.DeepMed.Mapping;
using ASMGX.DeepMed.Shared.Hashing.Concrete;
using ASMGX.DeepMed.Shared.Hashing.Interfaces;
using ASMGX.DeepMed.Shared.Hashing.Models;
using ASMGX.DeepMed.WebApp.API.Middlewares;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom Validation Middleware
builder.Services.AddValidationMiddleware();

//Custom Options
builder.Services.Configure<HashingOptions>(
    builder.Configuration.GetSection("HashingOptions"));


//Custom Services
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddManagers();
builder.Services.AddAutoMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Custom Exception Middleware
app.UseCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
