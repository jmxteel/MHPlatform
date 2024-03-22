using Installation.Domain.Context;
using Installation.Domain.IRepository;
using Installation.Domain.Repository;
using Installation.Domain.UOW;
using Installation.Service;
using Installation.Service.IService;
using Installation.Service.Service;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.IRepository;
using MHPlatform.Domain.Repository;
using MHPlatform.Service.IService;
using MHPlatform.Service.Model.Security;
using MHPlatform.Service.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InstallationContext>(
        dbContextOptions => dbContextOptions.UseSqlServer(
            builder.Configuration["ConnectionStrings:DBConnectionString"]));

builder.Services.AddScoped(typeof(IGeneric<>), typeof(Generic<>));
builder.Services.AddScoped<IFileFlowRepository, FileFlowRepository>();
builder.Services.AddScoped<IFileFlowAreaRepository, FileFlowAreaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UserAuthBase>();
builder.Services.AddScoped<ISecurityManager, SecurityManager>();

builder.Services.AddTransient(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddTransient<IFileFlowService, FileFlowService>();
builder.Services.AddTransient<IFileFlowAreaService, FileFlowAreaService>();
builder.Services.AddTransient<UserAuthBaseDto>();
builder.Services.AddTransient<ISecurityManagerService, SecurityManagerService>();

builder.Services.MHPlatformServices();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", jwtBearerOptions =>
{
    jwtBearerOptions.TokenValidationParameters =
    new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:key"])),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtToken:issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtToken:audience"],

        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration["JwtToken:minutestoexpiration"]))
    };
});

builder.Services.AddAuthorization(options => {
    //NOTE claim key and values are case sensitive
    options.AddPolicy("CanAccessProducts", p =>
    p.RequireClaim("CanAccessProducts", "true"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
