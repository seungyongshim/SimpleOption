using ClassLibrary1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
builder.Services.AddOptions<App1DbOption>().BindConfiguration("App1").ValidateDataAnnotations().ValidateOnStart();
builder.Services.AddOptions<App2DbOption>().BindConfiguration("App2").ValidateDataAnnotations().ValidateOnStart();
//builder.Services.AddOptions<App3DbOption>().BindConfiguration("App3").ValidateDataAnnotations().ValidateOnStart();

var app = builder.Build();

await app.StartAsync();


var ret1 = app.Services.GetRequiredService<IOptions<DbOption>>();
var ret2 = app.Services.GetRequiredService<IEnumerable<IOptions<DbOption>>>();
var ret3 = app.Services.GetRequiredService<IOptions<App1DbOption>>();
var ret4 = app.Services.GetRequiredService<IOptions<App2DbOption>>();
var ret5 = app.Services.GetRequiredService<IOptions<App3DbOption>>();

IOptions<DbOption> a = ret4;

await app.StopAsync();



public record App1DbOption : DbOption;
public record App2DbOption : DbOption;
public record App3DbOption : DbOption;
