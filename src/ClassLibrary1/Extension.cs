using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public static class Extension
{
    public static IServiceCollection AddSimpleOptions<T>(this IServiceCollection services, string configSessionName, Action<T, IServiceProvider>? postAction = null) where T : class
    {
        services.AddOptions<T>()
                .BindConfiguration(configSessionName)
                .Validate<IServiceProvider>((x, sp) =>
                {
                    var ctx = new ValidationContext<T>(x);
                    var validator = GetOptionValidator(x.GetType(), sp);

                    var ret = validator?.Validate(ctx);

                    return ret switch
                    {
                        { IsValid: false, Errors: { } e } => ErrorLogging(sp, e),
                        null                              => false,
                        { IsValid: true }                 => true
                    };
                })
                .ValidateDataAnnotations()
                .PostConfigure(postAction ?? ((a, b) => { }))
                .ValidateOnStart();

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<T>>().Value);

        return services;
    }

    public static bool ErrorLogging(IServiceProvider sp, IEnumerable<ValidationFailure> errors)
    {
        var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger("SimpleOptions");

        logger.LogError(new EventId(60010), new ValidationException("Validation Errors", errors), "");

        return false;
    }


    public static IValidator? GetOptionValidator(Type t, IServiceProvider sp) => t switch
    {
        { Name: "Object" } => null,
        _ => sp.GetService(typeof(IValidator<>).MakeGenericType(t)) switch
        {
            IValidator v => v,
            _ => t.BaseType switch
            {
                { } b => GetOptionValidator(b, sp),
                _     => null
            }
        }
    };
}


