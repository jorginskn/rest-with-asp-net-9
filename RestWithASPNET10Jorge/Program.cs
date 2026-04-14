using RestWithASPNET9Jorge.Configurations;
using RestWithASPNET9Jorge.Interfaces;
using RestWithASPNET9Jorge.Repositories;
using RestWithASPNET9Jorge.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.AddSeriloggerConfiguration();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
