using Agricargo.Application.Interfaces;
using Agricargo.Application.Services;
using Agricargo.Domain.Interfaces;
using Agricargo.Infrastructure.Data;
using Agricargo.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuración de JWT
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Configuración de autorización por roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
    options.AddPolicy("SysAdminPolicy", policy =>
        policy.RequireRole("SuperAdmin"));
    options.AddPolicy("ClientPolicy", policy =>
        policy.RequireRole("Client"));
    options.AddPolicy("AllPolicy", policy =>
    policy.RequireRole("Client", "Admin"));
});

// Agregar servicios de controladores
builder.Services.AddControllers(); 

// Configuración de Swagger y JWT
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT de esta forma: Bearer {token}"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

// Agregar otros servicios (repositorios, servicios de dominio, etc.)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IShipRepository, ShipRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IShipService, ShipService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IUserService, UserService>();

// Configurar la base de datos SQLite
var connection = new SqliteConnection("Data source = DbTest.db");
connection.Open();
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlite(connection, b => b.MigrationsAssembly("Agricargo.Infrastructure")));

// Configurar CORS
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

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Asegúrate de agregar autenticación
app.UseAuthorization();

app.UseCors("AllowAllOrigins");

// Mapear controladores
app.MapControllers(); // Mapea los controladores

app.Run();
