using Microsoft.EntityFrameworkCore;
using MottuApi.Application.Interfaces;
using MottuApi.Application.Services;
using MottuApi.Domain.Interfaces;
using MottuApi.Infrastructure.Data;
using MottuApi.Infrastructure.Repositories;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using MottuApi.Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Configurar validação de modelo
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "Mottu API", 
        Version = "v1",
        Description = @"
## 🚀 Mottu API - Sistema de Gerenciamento de Motos e Filiais

API RESTful desenvolvida em .NET 8 seguindo os princípios de **Clean Architecture** e **Domain-Driven Design**.

### 📋 Funcionalidades
- **Gerenciamento de Filiais**: CRUD completo com paginação e HATEOAS
- **Gerenciamento de Motos**: CRUD completo com busca por placa e filial
- **Validações de Domínio**: Regras de negócio encapsuladas nas entidades
- **Paginação**: Suporte a paginação em todos os endpoints de listagem
- **HATEOAS**: Links de navegação para melhor descoberta da API

### 🏗️ Arquitetura
- **Clean Architecture** com 4 camadas bem definidas
- **Domain-Driven Design** com entidades ricas
- **Repository Pattern** para abstração de dados
- **AutoMapper** para mapeamento de objetos
- **Entity Framework Core** com Oracle Database

### 📊 Endpoints Disponíveis
- **Filiais**: 7 endpoints (CRUD + ativar/desativar)
- **Motos**: 9 endpoints (CRUD + disponibilidade + busca)
- **Total**: 16 endpoints implementados

### 🔧 Tecnologias
- .NET 8
- Entity Framework Core 8.0.4
- Oracle Database
- AutoMapper 12.0.1
- Swagger/OpenAPI 6.5.0
        ",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Equipe Mottu",
            Email = "contato@mottu.com.br",
            Url = new Uri("https://www.mottu.com.br")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
    
    // Incluir comentários XML se existirem
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Configurar exemplos de schemas
    c.SchemaFilter<SwaggerExampleFilter>();
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

// Tornar a classe Program pública para testes
public partial class Program { }
