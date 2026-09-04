using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using QueueManagement.Api.ErrorHandling;
using QueueManagement.Api.Filters;

namespace QueueManagement.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1"
            });
            options.AddSecurityDefinition(
                "oauth2",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,

                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(configuration["SwaggerSettings:TokenUrl"]),
                            Scopes = new Dictionary<string, string>()
                        }
                    }
                });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("oauth2", document)] = []
            });
        });

        services.AddControllers(options => { options.Filters.Add<ValidationFilter>(); });

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        return services;
    }
}
