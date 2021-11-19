using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Riglog.Api.Filters;

public class SetVersionInPathsFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (swaggerDoc == null) throw new ArgumentNullException(nameof(swaggerDoc));

        var replacements = new OpenApiPaths();

        foreach (var (key, value) in swaggerDoc.Paths)
        {
            replacements.Add(key.Replace("v{version}", context.DocumentName, StringComparison.InvariantCulture), value);
        }

        swaggerDoc.Paths = replacements;
    }
}