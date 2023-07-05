using VideoToVfd.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();
app.MapHub<VfdHub>("/vfdhub");

VfdPlayer_Gpio.Start();

app.Run();
