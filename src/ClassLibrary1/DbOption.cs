using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace ClassLibrary1;


public record DbOption
{
    public required string DbConnRO { get; init; }
    public required string DbConnRW { get; init; }
}

public static class Extension
{
    public static IHostBuilder UseDatabase<TDbOption>(this IHostBuilder host, string configureSessionName) where TDbOption : DbOption
    {
        host.ConfigureServices((ctx, services) =>
        {
            services.TryAddSingleton<IValidator<DbOption>, DbOptionValidator>();
            services.AddSimpleOptions<TDbOption>(configureSessionName);
        });

        return host;
    }
}
