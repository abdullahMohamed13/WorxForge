using WorxForge.API.Configuration;
using WorxForge.API.Extensions;
using WorxForge.Core.Configuration;
using WorxForge.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAccessTokenAuthentication();
builder.Services.AddWorxForgeAuthorization();
builder.Services.AddWorxForgeCore();
builder.Services.AddWorxForgeInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseWorxForgePipeline();
app.MapWorxForgeEndpoints();

app.Run();
