using Microsoft.EntityFrameworkCore;
using NewsPortal.Infrastructure;
using NewsPortal.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructureServices();


var app = builder.Build();


app.Run();


public partial class Program { }