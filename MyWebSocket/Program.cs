using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:8888");

builder.Services.AddControllers();

var app = builder.Build();

var opt = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(5)
};

app.UseWebSockets(opt);

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();

