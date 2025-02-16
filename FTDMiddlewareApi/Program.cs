using System.Reflection;
using log4net;
using log4net.Config;
using FTDMiddlewareDataAccess.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using FTDMiddlewareApi.Service.Extensions;

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        // Get the first error field and message
        var firstErrorField = errors.Keys.FirstOrDefault();
        var firstErrorMessage = errors.Values.FirstOrDefault()?.FirstOrDefault() ?? "Validation failed";

        // Custom response format
        var response = new Base
        {
            Result = 0,
            ResultMessage = firstErrorMessage,
        };

        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // This will exclude properties with null values from the JSON response.
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;
builder.Services.AddCustomServices(); // Register all services

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();