using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Riglog.Api.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Riglog.Api.Configurations;

internal class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
        
    public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }
        
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo
                {
                    Title = "Riglog API",
                    Version = description.GroupName,
                    Description = "GitHub: [https://github.com/riglog](https://github.com/riglog)"
                }
            );

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        }
            
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "No need to put the `Bearer` keyword in front of token",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });

        options.OperationFilter<AuthorizationOperationFilter>();
        options.OperationFilter<RemoveVersionParametersFilter>();
        options.DocumentFilter<SetVersionInPathsFilter>();
    }
}