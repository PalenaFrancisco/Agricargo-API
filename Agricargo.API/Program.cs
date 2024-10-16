

using Agricargo.Application.Interfaces;
using Agricargo.Application.Services;
using Agricargo.Domain.Interfaces;
using Agricargo.Infrastructure.Data;
using Agricargo.Infrastructure.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IShipRepository, ShipRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<ITripService, TripService>();

var connection = new SqliteConnection("Data source = DbTest.db");
connection.Open();
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlite(connection, b => b.MigrationsAssembly("Agricargo.Infrastructure")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
