using Microsoft.EntityFrameworkCore;
using MottuApi.Data;
using MottuApi.Repositories;
using MottuApi.Repositories.Interfaces;
using MottuApi.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Configuração do EF Core com Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Repositórios e Services
builder.Services.AddScoped<IFilialRepository, FilialRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<FilialService>();
builder.Services.AddScoped<MotoService>();

// Controllers
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
