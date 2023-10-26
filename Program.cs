﻿using Microsoft.EntityFrameworkCore;
using Well_Up_API.Models;
using Well_Up_API.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAnyOrigin", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PostgresDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("PosgresDB")));

builder.Services.AddScoped<TestService>();
builder.Services.AddScoped<MoodService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAnyOrigin");
app.MapControllers();

app.Run();

