namespace Gamba.WebAPI.Features.SeedWork;

public interface IFeatureModule
{
    IServiceCollection RegisterModule(IServiceCollection services);
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
}