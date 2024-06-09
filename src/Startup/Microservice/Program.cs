using System.Net.Mime;
using System.Text.Json;
using Microservice.Application.Settings;
using Microservice.Configuration.Auth;
using Microservice.Configuration.Database;
using Microservice.Configuration.Dependencies;
using Microservice.Configuration.Swagger;
using Microservice.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

// TODO: If you using .NET Aspire provide the namespace for the project
// using ServiceDefaults

var builder = WebApplication.CreateBuilder(args);

//Add logger
builder
    .Configuration
    .AddJsonFile("logsettings.json");

// TODO: If you are not using .NET Aspire, you can uncomment the following line
// to configure Serilog OR If it's not configured in the
// ServiceDefaults project.
//builder
//    .Host
//    .UseSerilog
//    (
//        (host, logger) =>
//            logger
//                .ReadFrom.Configuration(host.Configuration)
//                .Enrich.WithCorrelationIdHeader()
//    )

// this method is called by multiple projects
// serilog settings has been moved here, as all projects
// would need it
// TODO: if you using .NET Aspire and have added the ServiceDefaults project
// uncomment the following line
// builder.AddServiceDefaults()

var services = builder.Services;
var configuration = builder.Configuration;

var appSettingsSection = configuration.GetSection(nameof(AppSettings));
var appSettings = appSettingsSection.Get<AppSettings>();

// adds sql server database context
services.AddDatabase(configuration);

// Add services to the container.
services.AddServices();

services.AddHttpContextAccessor();

// routing configuration
services.AddRouting(options => options.LowercaseUrls = true);

// configures the authentication and authorization
services.AddAuthenticationAndAuthorization(appSettings!);

services
    .AddControllers(options =>
    {
        options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
        options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
        options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
        options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status403Forbidden));
        options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
        options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status503ServiceUnavailable));
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    });

// add default health checks
services.Configure<HealthCheckPublisherOptions>(options => options.Period = TimeSpan.FromSeconds(300));

services.AddHealthChecks();

// adds the Swagger for the Api Documentation
services.AddSwagger();

var app = builder.Build();

// TODO: if you using .NET Aspire and have added the ServiceDefaults project
// uncomment the following line
// app.MapDefaultEndpoints()

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// TODO: If you have a specific package or project
// referenced that allows injecting the correlationId in the request
// pipeline, uncomment the following and rename the method if necessary.
// app.UseCorrelationId()

app.UseRouting();

app.UseAuthentication();

// TODO: If you have a specific package or project
// referenced that allows the request tracing like Serilog request tracing
// uncomment the following and rename the method if necessary.
// NOTE: Needs to be after UseAuthentication in the pipeline so it can extract the claims values
// if needed
// app.UseRequestTracing()

app.UseAuthorization();

app.MapControllers();

// run the database migration and seed the data
await app.MigrateAndSeedDatabaseAsync();

await app.RunAsync();