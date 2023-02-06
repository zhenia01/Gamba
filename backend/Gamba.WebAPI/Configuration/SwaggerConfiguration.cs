using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Gamba.WebAPI.Configuration;

public class AuthOperationsFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authRequired = context.ApiDescription.ActionDescriptor.EndpointMetadata
            .Any(attr => attr.GetType() == typeof(AuthorizeAttribute));

        operation.Security = authRequired
            ? new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            }
            : new List<OpenApiSecurityRequirement>();
    }
}

public static class SwaggerConfiguration
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then " +
                              "your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });

            c.OperationFilter<AuthOperationsFilter>();

            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gamba.WebAPI", Version = "v1" });
        });
    }
    
    public static void UseSwaggerDev(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}