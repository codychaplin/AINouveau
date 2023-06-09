﻿using Microsoft.EntityFrameworkCore;
using AINouveau.Server.Data;
using AINouveau.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AINouveauDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AINouveauDbContext") ??
    throw new InvalidOperationException("Connection string 'AINouveauDbContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IArtworkService, ArtworkService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
