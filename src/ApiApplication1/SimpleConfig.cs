using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;


public interface ISimpleConfig<>
{

}
public static class SimpleConfig
{
    public static IServiceCollection AddSimpleConfig(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Singleton(typeof(ISimpleConfig<,>), typeof(UnnamedOptionsManager<>)));


        services.TryAdd(ServiceDescriptor.Scoped(typeof(IOptionsSnapshot<>), typeof(OptionsManager<>)));
        services.TryAdd(ServiceDescriptor.Singleton(typeof(IOptionsMonitor<>), typeof(OptionsMonitor<>)));
        services.TryAdd(ServiceDescriptor.Transient(typeof(IOptionsFactory<>), typeof(OptionsFactory<>)));
        services.TryAdd(ServiceDescriptor.Singleton(typeof(IOptionsMonitorCache<>), typeof(OptionsCache<>)));
        return services;
    }
}
