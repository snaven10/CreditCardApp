using CreditCardApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Web;
using HealthChecks.ApplicationStatus.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using CreditCardApp.Application.Interfaces;
using MediatR;
using System.Reflection;
using CreditCardApp.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Registrar FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        config.DisableDataAnnotationsValidation = true;
    });

// Registro de MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Registro del DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<PdfGeneratorService>();
// Configuraci�n de health checks
builder.Services
    .AddHealthChecks()
    .AddApplicationStatus(name: "api_status", tags: new[] { "api" })
    .AddSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection")!,
        name: "sql",
        failureStatus: HealthStatus.Degraded,
        tags: new[] { "db", "sql", "sqlserver" })
    .AddCheck<ServerHealthCheck>("server_health_check", tags: new[] { "custom", "api" });

builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(builder.Configuration.GetValue<int>("HealthChecksUI:EvaluationTimeInSeconds"));
    options.MaximumHistoryEntriesPerEndpoint(50);
})
.AddInMemoryStorage();

var app = builder.Build();

app.MapGet("/", () => "�Hola, mundo!");
// Map HealthChecks endpoints
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(config =>
{
    config.UIPath = "/health-ui";
});

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
