using Installation.Domain.Context;
using Installation.Domain.IRepository;
using Installation.Domain.Repository;
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

builder.Services.AddScoped<IFileFlowRepository, FileFlowRepository>();
builder.Services.AddScoped(typeof(IGeneric<>), typeof(Generic<>));

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
