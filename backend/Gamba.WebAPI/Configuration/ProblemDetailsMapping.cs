using System.Net;
using System.Security.Authentication;
using Gamba.Application.Exceptions;
using Gamba.Domain.BuildingBlocks;
using Gamba.WebAPI.SeedWork;
using Hellang.Middleware.ProblemDetails;

namespace Gamba.WebAPI.Configuration;

public static class ProblemDetailsMapping
{
    public static IServiceCollection MapProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(opt =>
        {
            opt.MapToStatusCode<AuthenticationException>(StatusCodes.Status401Unauthorized);
            opt.MapToStatusCode<EntityNotFoundException>(StatusCodes.Status404NotFound);
            opt.Map<BusinessRuleValidationException>(e => new BusinessRuleValidationExceptionProblemDetails(e));
            opt.Map<ArgumentException>((_, e) => e.Source == "Dawn.Guard",
                (_, _) => StatusCodeProblemDetails.Create(StatusCodes.Status422UnprocessableEntity));
        });

        return services;
    }
}