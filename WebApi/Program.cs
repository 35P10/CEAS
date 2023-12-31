using Domain.Application.Contracts;
using Domain.Infra.Integration.Factories;
using Infra.Integration;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
                    {
                        options.AddPolicy("AllowAnyOrigin",
                            builder => builder.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader());
                    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();

builder.Services.AddIntegrationInfrastructureServices(builder.Configuration);

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
