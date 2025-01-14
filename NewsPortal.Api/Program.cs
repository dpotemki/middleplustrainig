using NewsPortal.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.AddInfrastructureServices();

app.Run();