using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Application.Command.Factory;
using Application.WebSocketHandler;
using Domain.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public class WebSocketRoutes
{
    private readonly WebSocketHandler _webSocketHandler;

    public WebSocketRoutes(WebSocketHandler webSocketHandler)
    {
        _webSocketHandler = webSocketHandler;
    }

    public void MapWebSocketRoutes(WebApplication app)
    {
        app.MapGet("/ws", async context =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await _webSocketHandler.Echo(context, webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        });

    }
}