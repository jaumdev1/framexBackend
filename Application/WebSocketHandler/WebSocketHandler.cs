using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Application.Command.Factory;
using Microsoft.AspNetCore.Http;
using Domain.Entity;
namespace Application.WebSocketHandler;

public class WebSocketHandler
{
    private readonly CommandFactory _factory;

    public WebSocketHandler(CommandFactory factory)
    {
        _factory = factory;
    }

    public async Task Echo(HttpContext context, WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            
            var command = _factory.CreateCommand(message);
            
            await command.Execute(webSocket);
            
            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }
        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
}