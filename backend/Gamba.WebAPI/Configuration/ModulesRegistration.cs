using Gamba.WebAPI.Features.SeedWork;

namespace Gamba.WebAPI.Configuration;

// https://timdeschryver.dev/blog/maybe-its-time-to-rethink-our-project-structure-with-dot-net-6#register-the-modules-automatically
public static class ModulesRegistration
{
    // this could also be added into the DI container
    private static readonly List<IFeatureModule> RegisteredModules = new ();
 
    public static IServiceCollection AddFeatureModules(this IServiceCollection services)
    {
        var modules = DiscoverModules();
        foreach (var module in modules)
        {
            module.RegisterModule(services);
            RegisteredModules.Add(module);
        }
 
        return services;
    }
 
    public static RouteGroupBuilder MapFeatureModulesEndpoints(this RouteGroupBuilder routeGroupBuilder)
    {
        foreach (var module in RegisteredModules)
        {
            module.MapEndpoints(routeGroupBuilder);
        }
        return routeGroupBuilder;
    }
 
    private static IEnumerable<IFeatureModule> DiscoverModules()
    {
        return typeof(IFeatureModule).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IFeatureModule)))
            .Select(Activator.CreateInstance)
            .Cast<IFeatureModule>();
    }
}