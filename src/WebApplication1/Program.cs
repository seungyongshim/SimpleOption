using ClassLibrary1;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Host.UseDatabase<App1DbOption>("App1");
builder.Host.UseDatabase<App2DbOption>("App2");
builder.Host.UseDatabase<App3DbOption>("App3");

var app = builder.Build();

await app.StartAsync();


_ = app.Services.GetRequiredService<App1DbOption>();
_ = app.Services.GetRequiredService<App2DbOption>();
_ = app.Services.GetRequiredService<App3DbOption>();

await app.StopAsync();



public record App1DbOption : DbOption;
public record App2DbOption : DbOption;
public record App3DbOption : DbOption;
