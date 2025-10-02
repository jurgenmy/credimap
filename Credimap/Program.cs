using System.Globalization;
using Microsoft.AspNetCore.SignalR;
using Credimap.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddCors(options =>
{
	options.AddPolicy("DevCors", policy =>
	{
		policy
			.AllowAnyOrigin()
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});

builder.Services.AddControllers();
builder.Services.AddSignalR();

var app = builder.Build();

// Middleware
app.UseCors("DevCors");
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

// SignalR hubs
app.MapHub<LocationHub>("/hubs/location");

app.Run();
