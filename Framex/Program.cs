using Application.Command.Factory;
using Application.Services;
using Application.WebSocketHandler;
using static Framex.Routes.GlobalRoutes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<CommandFactory>();
builder.Services.AddTransient<WebSocketHandler>(); 
builder.Services.AddTransient<WebSocketRoutes>();
builder.Services.AddSingleton<GameService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWebSockets();
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseRouting();
app.MapGlobalRoutes();

app.Run();