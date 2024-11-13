using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.Extensions;
using UIT.CodeRelax.Infrastructure.Repositories;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.Mapper;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Impls;
using UIT.CodeRelax.UseCases.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add request validator
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<LoginReq>, LoginReqValidator>();

// Inject services and repository
builder.Services.AddScoped<IProblemService, ProblemService>();
builder.Services.AddScoped<ITestcaseRepository, TestcaseRepository>();
builder.Services.AddScoped<IProblemRepository, ProblemRepository>();

builder.Services.AddScoped<ITagRespository, TagRepository>();
builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddAutoMapper(typeof(AppProfile));




//config log 
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5173");
        builder.WithMethods("GET", "POST", "PUT", "DELETE");
        builder.AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
