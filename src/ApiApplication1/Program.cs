var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddOptions();

var app = builder.Build();

app.Run();
