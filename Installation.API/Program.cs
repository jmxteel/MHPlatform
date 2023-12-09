using Installation.Domain.Context;
using Installation.Domain.IRepository;
using Installation.Domain.Repository;
using Installation.Domain.UOW;
using Installation.Service;
using Installation.Service.IService;
using Installation.Service.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddTransient(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddTransient<IFileFlowService, FileFlowService>();
builder.Services.AddTransient<IFileFlowAreaService, FileFlowAreaService>();

builder.Services.MHPlatformServices();
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
