﻿using Microsoft.OpenApi.Models;
using Orleans.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleans((_, silo) => silo
    .UseLocalhostClustering()
    .AddMemoryGrainStorageAsDefault()
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Example eShop API by single team", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
    _ = app.UseSwagger().UseSwaggerUI(options => options.EnableTryItOutByDefault());

app.UseAuthorization();

app.MapControllers();

app.Run();

sealed partial class Program { } // Fix CA1852