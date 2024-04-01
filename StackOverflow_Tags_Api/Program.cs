using Microsoft.EntityFrameworkCore;
using StackOverflow_Tags_Api.controlers;
using StackOverflow_Tags_Api.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddConsole();

// Add services to the container.
builder.Services.AddDbContext<StackOverflow_Tags_ApiContext>(options =>
    options.UseSqlServer(builder.Configuration["DatabaseConnection"] ??
    throw new InvalidOperationException("Connection string 'StackOverflow_Tags_ApiContext' not found.")));
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "StackOverflow_Tags_Api", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapTagEndpoints();

app.Run();