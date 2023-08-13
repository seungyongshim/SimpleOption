using ClassLibrary1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSimpleOptions<App1DbOption>("App1");
builder.Services.AddSimpleOptions<App2DbOption>("App2");
builder.Services.AddSimpleOptions<App3DbOption>("App3", (o, sp) =>
{

});

var app = builder.Build();

await app.StartAsync();


var a = app.Services.GetRequiredService<App1DbOption>();
var b = app.Services.GetRequiredService<App2DbOption>();
var c = app.Services.GetRequiredService<App3DbOption>();

await app.StopAsync();



public record App1DbOption : DbOption;
public record App2DbOption : DbOption;
public record App3DbOption : DbOption;
