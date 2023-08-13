using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public static class Extension
{
    public static IServiceCollection AddSimpleOptions<T>(this IServiceCollection services, string configSessionName, Action<T, IServiceProvider>? postAction= null) where T : class
    {
        services.AddOptions<T>()
                .BindConfiguration(configSessionName)
                .Validate<IServiceProvider>((x, sp) =>
                {
                    var validator = sp.GetRequiredService<IValidator<T>>();

                    var ret = validator.Validate(x);

                    return ret.IsValid;
                })
                .ValidateDataAnnotations()
                .PostConfigure(postAction ?? ((a, b) => { }))
                .ValidateOnStart();

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<T>>().Value);

        return services;
    }
}


