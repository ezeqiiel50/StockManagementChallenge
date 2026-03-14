using StockManagerApi.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSolucion();
var app = builder.Build();
app.ConfigureWebApplication();
app.Run();