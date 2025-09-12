using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using MottuApi.Application.DTOs;

namespace MottuApi.Presentation.Filters
{
    public class SwaggerExampleFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(CreateFilialDTO))
            {
                schema.Example = new OpenApiObject
                {
                    ["nome"] = new OpenApiString("Filial São Paulo"),
                    ["logradouro"] = new OpenApiString("Rua das Flores"),
                    ["numero"] = new OpenApiString("123"),
                    ["complemento"] = new OpenApiString("Sala 1"),
                    ["bairro"] = new OpenApiString("Centro"),
                    ["cidade"] = new OpenApiString("São Paulo"),
                    ["estado"] = new OpenApiString("SP"),
                    ["cep"] = new OpenApiString("01234567"),
                    ["telefone"] = new OpenApiString("(11) 99999-9999")
                };
            }
            else if (context.Type == typeof(UpdateFilialDTO))
            {
                schema.Example = new OpenApiObject
                {
                    ["nome"] = new OpenApiString("Filial São Paulo Atualizada"),
                    ["logradouro"] = new OpenApiString("Rua das Flores Atualizada"),
                    ["numero"] = new OpenApiString("456"),
                    ["complemento"] = new OpenApiString("Sala 2"),
                    ["bairro"] = new OpenApiString("Centro"),
                    ["cidade"] = new OpenApiString("São Paulo"),
                    ["estado"] = new OpenApiString("SP"),
                    ["cep"] = new OpenApiString("01234567"),
                    ["telefone"] = new OpenApiString("(11) 88888-8888")
                };
            }
            else if (context.Type == typeof(CreateMotoDTO))
            {
                schema.Example = new OpenApiObject
                {
                    ["placa"] = new OpenApiString("ABC1234"),
                    ["modelo"] = new OpenApiString("Honda CG 160"),
                    ["ano"] = new OpenApiInteger(2023),
                    ["cor"] = new OpenApiString("Vermelha"),
                    ["filialId"] = new OpenApiInteger(1)
                };
            }
            else if (context.Type == typeof(UpdateMotoDTO))
            {
                schema.Example = new OpenApiObject
                {
                    ["modelo"] = new OpenApiString("Honda CG 160 Titan"),
                    ["ano"] = new OpenApiInteger(2024),
                    ["cor"] = new OpenApiString("Azul")
                };
            }
        }
    }
}
