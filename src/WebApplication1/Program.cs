var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();

var app = builder.Build();

app.Run();

