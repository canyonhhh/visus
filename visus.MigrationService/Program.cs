using Microsoft.EntityFrameworkCore;
using visus.Data.Contexts;
using visus.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<AppDbContext>("postgresdb");

var host = builder.Build();
host.Run();