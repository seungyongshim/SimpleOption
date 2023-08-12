using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WebApplication1;


public interface IConfigFactory<T>
{
    T Create<TAppName>(TAppName appName) where TAppName : struct, Enum => Create(Enum.GetName(appName)!);
    T Create(string appName);
}

public class ConfigFactory<T> : IConfigFactory<T>
{
    public T Create(string appName) => 
}

public static class SimpleConfigExtenions
{
    public static IServiceCollection AddConfigFactory(this IServiceCollection services)
    {
        services.TryAddSingleton(typeof(IConfigFactory<>, ConfigFactory<>));
    }
}
