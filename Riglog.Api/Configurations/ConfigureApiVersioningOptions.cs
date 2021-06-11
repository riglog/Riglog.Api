using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;

namespace Riglog.Api.Configurations
{
    public class ConfigureApiVersioningOptions : IConfigureOptions<ApiVersioningOptions>
    {
        public void Configure(ApiVersioningOptions options)
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = ApiVersion.Default;
            options.ApiVersionReader = new MediaTypeApiVersionReader("x-api-version");
        }
    }
}