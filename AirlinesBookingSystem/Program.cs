using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using AirlinesBookingSystem;


var builder = WebApplication.CreateBuilder();
//builder.Services.AddHostedService<Worker>();

builder.Services.AddControllers();


var host = builder.Build();

host.MapControllers();

host.Run();