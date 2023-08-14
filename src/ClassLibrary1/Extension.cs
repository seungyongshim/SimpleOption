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
                    var ctx = new ValidationContext<T>(x);

                    var validator = GetOptionValidator(x.GetType(), sp);

                    return validator?.Validate(ctx).IsValid ?? false;
                })
                .ValidateDataAnnotations()
                .PostConfigure(postAction ?? ((a, b) => { }))
                .ValidateOnStart();

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<T>>().Value);

        return services;
    }

    public static IValidator? GetOptionValidator(Type t, IServiceProvider sp)
    {
        if (t is { Name :"Object"}) return null;

        var validator = sp.GetService(typeof(IValidator<>).MakeGenericType(t));

        return validator switch
        {
            IValidator v => v,
            _            => GetOptionValidator(t.BaseType, sp)
        };
    }
}


