using Logistics.Api.Infrastructure;
using Logistics.Modules.Ordering.Infrastructure;
using Logistics.Modules.Shipping.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Logistics.Modules.Ordering;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); // Generates standardized error responses

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOrderingModule(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(); 

app.MapOrderingEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
