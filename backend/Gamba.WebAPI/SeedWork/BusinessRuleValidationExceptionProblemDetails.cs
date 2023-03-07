using Gamba.Domain.BuildingBlocks;

namespace Gamba.WebAPI.SeedWork
{
    public class BusinessRuleValidationExceptionProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            Title = "Business rule validation error";
            Status = StatusCodes.Status409Conflict;
            Detail = exception.Details;
            Extensions["error"] = exception.BrokenRule.GetType().Name[..^4];
        }
    }
}