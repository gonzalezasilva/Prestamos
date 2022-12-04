using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplicationPrestamos.Configuration;
using WebApplicationPrestamos.DataAccess;
using WebApplicationPrestamos.Models;
using WebApplicationPrestamos.Handlers;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Serialization;
using WebApplicationPrestamos.Protos;
using Microsoft.AspNetCore.Builder;
using WebApplicationPrestamos.Services;

var builder = WebApplication.CreateBuilder(args);


// middleware de encabezados
var MyAllowSpecificOrigins = "_MyAllowSubdomainPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader() .AllowAnyMethod();
           // policy.WithOrigins("http://localhost:4200")
           //        .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<ThingsContext>(options =>
{
    //Para poder utilizar SqlServer necesitamos instalar el paquete
    //Microsoft.EntityFrameworkCore.SqlServer
    options.UseSqlServer(builder.Configuration.GetConnectionString("ThingsContextConnection"));
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});


builder.Services.AddAuthorization();

//Agregando configuracion para JWT
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<ILoanService, LoanService>();
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

//Creando la aplicacion.
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapGrpcReflectionService<ReturnLoanService>();
app.MapGrpcService<ReturnLoanService>();  


app.Run();
