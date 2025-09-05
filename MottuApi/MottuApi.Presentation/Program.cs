using Microsoft.EntityFrameworkCore;
using MottuApi.Application.Interfaces;
using MottuApi.Application.Services;
using MottuApi.Domain.Interfaces;
using MottuApi.Infrastructure.Data;
using MottuApi.Infrastructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "Mottu API", 
        Version = "v1",
        Description = "API para gerenciamento de motos e filiais da Mottu",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipe Mottu",
            Email = "contato@mottu.com.br"
        }
    });
    
    // Incluir comentários XML se existirem
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(MottuApi.Application.Mappings.MappingProfile));

// Configuração do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Registro dos serviços
builder.Services.AddScoped<IFilialRepository, FilialRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IFilialService, FilialService>();
builder.Services.AddScoped<IMotoService, MotoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu API v1");
        c.RoutePrefix = string.Empty; // Para acessar o Swagger na raiz
        c.DocumentTitle = "Mottu API - Documentação";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
