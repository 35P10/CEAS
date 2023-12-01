using Domain.Application.Contracts;
using Domain.Infra.Integration.Factories;
using Infra.Integration;
using Microsoft.AspNetCore.Builder;
using Domain.Application.Models;
using Microsoft.AspNetCore.ResponseCompression;

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


builder.Services.AddSignalR();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
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

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseResponseCompression();
app.MapHub<CEAShub>("/hubs/ceashub")
    .RequireCors("AllowAnyOrigin");
app.MapControllers();

app.Run();
