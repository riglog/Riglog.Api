﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Riglog.Api.Configurations;
using Riglog.Api.Data.Sql;
using Riglog.Api.Data.Sql.Interfaces;
using Riglog.Api.Data.Sql.Repositories;
using Riglog.Api.Services;
using Riglog.Api.Services.Interfaces;
using Riglog.Api.Services.Mappings;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Riglog.Api;

public class Startup
{
    private const string SwaggerBasePath = "api";
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {

        if (Configuration["Settings:Database"] == "PostgreSQL")
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlDatabase"),
                    opts => opts.CommandTimeout((int)TimeSpan.FromSeconds(20).TotalSeconds)
                        .MigrationsAssembly("Riglog.Api.Data.Sql.Postgres")));
        }
        else
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("SqlDatabase"),
                opts => opts.CommandTimeout((int)TimeSpan.FromSeconds(20).TotalSeconds)
                    .MigrationsAssembly("Riglog.Api.Data.Sql")));
        }
        
        services.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");

        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        services.AddSingleton<IConfigureOptions<ApiVersioningOptions>, ConfigureApiVersioningOptions>();
        services.AddApiVersioning();

        services.AddSingleton<IConfigureOptions<AuthenticationOptions>, ConfigureAuthenticationOptions>();
        services.AddAuthentication().AddJwtBearer();
        services.ConfigureOptions<ConfigureJwtBearerOptions>();

        services.AddControllers();

        services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        services.AddSwaggerGen();

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<IOsFamilyRepository, OsFamilyRepository>();
        services.AddScoped<IOsDistributionRepository, OsDistributionRepository>();
        services.AddScoped<IOsEditionRepository, OsEditionRepository>();
        services.AddScoped<IOsVersionRepository, OsVersionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IComputerTypeRepository, ComputerTypeRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDbService, DbService>();
        services.AddScoped<ISeedService, SeedService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger(c =>
        {
            c.RouteTemplate = SwaggerBasePath + "/{documentName}/swagger/swagger.json";
            c.PreSerializeFilters.Add((swaggerDoc, httpReq) => { swaggerDoc.Servers = new List<OpenApiServer> { new() { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } }; });
        });

        app.UseSwaggerUI(c =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.OrderByDescending(x => x.ApiVersion))
            {
                c.SwaggerEndpoint($"/{SwaggerBasePath}/{description.GroupName}/swagger/swagger.json", "Riglog API " + description.GroupName.ToUpperInvariant());
                c.RoutePrefix = $"{SwaggerBasePath}/swagger";
            }
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}