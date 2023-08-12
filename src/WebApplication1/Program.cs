using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
builder.Services.AddOptions<App1DbOption>().BindConfiguration("App1");
builder.Services.AddOptions<App2DbOption>().BindConfiguration("App2");

var app = builder.Build();

var ret1 = app.Services.GetRequiredService<IOptions<DbOption>>();
var ret2 = app.Services.GetRequiredService<IEnumerable<IOptions<DbOption>>>();
var ret3 = app.Services.GetRequiredService<IOptions<App1DbOption>>();
var ret4 = app.Services.GetRequiredService<IOptions<App2DbOption>>();


app.Run();


public record DbOption
{
    public string DbConnRO { get; init; }
    public string DbConnRW { get; init; }
}

public record App1DbOption : DbOption;
public record App2DbOption : DbOption;
