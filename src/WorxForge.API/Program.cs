using FluentValidation;
using Serilog;
using WorxForge.API.Configuration;
using WorxForge.API.Extensions;
using WorxForge.Core.Configuration;
using WorxForge.Infrastructure.Configuration;
using WorxForge.Infrastructure.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssembly(typeof(WorxForge.Core.Configuration.DependencyInjection).Assembly);
builder.Services.AddJwtAccessTokenAuthentication();
builder.Services.AddWorxForgeAuthorization();
builder.Services.AddWorxForgeCore();
builder.Services.AddWorxForgeInfrastructure(builder.Configuration);

var app = builder.Build();

await app.Services.SeedWorxForgeDataAsync();

app.UseWorxForgePipeline();
app.MapWorxForgeEndpoints();

app.Run();
