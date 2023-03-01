﻿using System.Net;
using System.Security.Authentication;
using Gamba.Application.Exceptions;
using Gamba.DataAccess.BuildingBlocks;
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
            opt.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
        });
        
        return services;
    }
}