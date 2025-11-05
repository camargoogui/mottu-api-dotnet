using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Driver;
using MottuApi.Application.Interfaces;
using MottuApi.Application.Services;
using MottuApi.Domain.Interfaces;
using MottuApi.Infrastructure.Data;
using MottuApi.Infrastructure.HealthChecks;
using MottuApi.Infrastructure.Repositories;
using MottuApi.Presentation.Filters;
using System.Text.Json.Serialization;

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

// Configurar versionamento da API
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver")
    );
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "Mottu API", 
        Version = "v1.0",
        Description = @"
## 🚀 Mottu API v1.0 - Sistema de Gerenciamento de Motos e Filiais

API RESTful desenvolvida em .NET 8 seguindo os princípios de **Clean Architecture** e **Domain-Driven Design**.

### 📋 Funcionalidades
- **Gerenciamento de Filiais**: CRUD completo com paginação e HATEOAS
- **Gerenciamento de Motos**: CRUD completo com busca por placa e filial
- **Gerenciamento de Locações**: CRUD completo com operações de estado
- **Validações de Domínio**: Regras de negócio encapsuladas nas entidades
- **Paginação**: Suporte a paginação em todos os endpoints de listagem
- **HATEOAS**: Links de navegação para melhor descoberta da API
- **Health Check**: Monitoramento da aplicação e banco de dados
- **Versionamento**: Suporte a múltiplas versões da API

### 🏗️ Arquitetura
- **Clean Architecture** com 4 camadas bem definidas
- **Domain-Driven Design** com entidades ricas
- **Repository Pattern** para abstração de dados
- **AutoMapper** para mapeamento de objetos
- **MongoDB** como banco de dados NoSQL

### 📊 Endpoints Disponíveis
- **Filiais**: 7 endpoints (CRUD + ativar/desativar)
- **Motos**: 9 endpoints (CRUD + disponibilidade + busca)
- **Locações**: 15 endpoints (CRUD + operações específicas)
- **Health Check**: 1 endpoint de monitoramento
- **Total**: 32 endpoints implementados

### 🔧 Tecnologias
- .NET 8
- MongoDB Driver 2.28.0
- AutoMapper 12.0.1
- Swagger/OpenAPI 6.5.0
- Health Checks
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

    c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "Mottu API", 
        Version = "v2.0",
        Description = @"
## 🚀 Mottu API v2.0 - Sistema de Gerenciamento de Motos e Filiais

Versão 2.0 da API com melhorias e novas funcionalidades.

### 🆕 Novidades na v2.0
- **Melhorias de Performance**: Otimizações nas consultas MongoDB
- **Novos Endpoints**: Funcionalidades adicionais para relatórios
- **Validações Aprimoradas**: Validações mais robustas
- **Documentação Melhorada**: Exemplos mais detalhados

### 📋 Funcionalidades Mantidas
- **Gerenciamento de Filiais**: CRUD completo com paginação e HATEOAS
- **Gerenciamento de Motos**: CRUD completo com busca por placa e filial
- **Gerenciamento de Locações**: CRUD completo com operações de estado
- **Health Check**: Monitoramento da aplicação e banco de dados
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
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }

    // Segurança via API Key no Swagger
    var apiKeyScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "X-API-KEY",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Chave de API para acesso aos endpoints",
        Reference = new Microsoft.OpenApi.Models.OpenApiReference
        {
            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
            Id = "ApiKeyScheme"
        }
    };

    c.AddSecurityDefinition("ApiKeyScheme", apiKeyScheme);
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        { apiKeyScheme, new List<string>() }
    });

    // Configurar exemplos de schemas
    c.SchemaFilter<SwaggerExampleFilter>();
});

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(MottuApi.Application.Mappings.MappingProfile));

// Configuração do MongoDB
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoConnection") ?? "mongodb://localhost:27017";
var mongoDatabaseName = builder.Configuration["MongoDatabaseName"] ?? "mottu_db";

builder.Services.AddSingleton<IMongoClient>(provider => new MongoClient(mongoConnectionString));
builder.Services.AddScoped<MongoDbContext>(provider =>
{
    var mongoClient = provider.GetRequiredService<IMongoClient>();
    return new MongoDbContext(mongoClient, mongoDatabaseName);
});

// Health Checks
builder.Services.AddHealthChecks()
    .AddCheck<MongoHealthCheck>("mongodb", tags: new[] { "database", "mongodb" })
    .AddCheck("application", () => HealthCheckResult.Healthy("Aplicação está funcionando"), tags: new[] { "application" });

// Registro dos serviços
builder.Services.AddScoped<IFilialRepository, FilialMongoRepository>();
builder.Services.AddScoped<IMotoRepository, MotoMongoRepository>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoMongoRepository>();
builder.Services.AddScoped<IFilialService, FilialService>();
builder.Services.AddScoped<IMotoService, MotoService>();
builder.Services.AddScoped<ILocacaoService, LocacaoService>();
builder.Services.AddSingleton<ILocacaoPredictionService, LocacaoPredictionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mottu API v1.0");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Mottu API v2.0");
        c.RoutePrefix = string.Empty; // Para acessar o Swagger na raiz
        c.DocumentTitle = "Mottu API - Documentação";
        c.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

// API Key Security Middleware (bypassa Swagger e Health)
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value ?? string.Empty;
    var isSwagger = path.StartsWith("/swagger") || path == "/";
    var isHealth = path.StartsWith("/health");

    if (isSwagger || isHealth)
    {
        await next();
        return;
    }

    var configuredKey = app.Configuration["ApiKey"];
    if (string.IsNullOrWhiteSpace(configuredKey))
    {
        await next();
        return;
    }

    if (!context.Request.Headers.TryGetValue("X-API-KEY", out var providedKey) || providedKey != configuredKey)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsJsonAsync(new { error = "API Key inválida ou ausente." });
        return;
    }

    await next();
});

app.UseAuthorization();

// Health Check endpoints
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                description = entry.Value.Description,
                duration = entry.Value.Duration.TotalMilliseconds
            }),
            totalDuration = report.TotalDuration.TotalMilliseconds
        };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
    }
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("database"),
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                description = entry.Value.Description
            })
        };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
    }
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("application"),
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                description = entry.Value.Description
            })
        };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
    }
});

app.MapControllers();

app.Run();

// Tornar a classe Program pública para testes
public partial class Program { }
