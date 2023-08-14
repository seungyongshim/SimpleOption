using ClassLibrary1;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IValidator<DbOption>, DbOptionValidator>();

builder.Host.UseDatabase<App1DbOption>("App1");
builder.Host.UseDatabase<App2DbOption>("App2");
builder.Host.UseDatabase<App3DbOption>("App3");

var app = builder.Build();

await app.StartAsync();


var a = app.Services.GetRequiredService<App1DbOption>();
var b = app.Services.GetRequiredService<App2DbOption>();
var c = app.Services.GetRequiredService<App3DbOption>();

await app.StopAsync();



public record App1DbOption : DbOption;
public record App2DbOption : DbOption;
public record App3DbOption : DbOption;
