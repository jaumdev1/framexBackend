using System.Data;
using System.Net.WebSockets;
using System.Text;
using Domain.Entity;
using Domain.Interfaces;
using System.Text.Json;
namespace Application.Command;

public class AttackCommand : ICommand
{
    public CommandBase Command { get; set; }

    public AttackCommand(string data)
    {
        Command = JsonSerializer.Deserialize<CommandBase>(data);
    }

    public async Task Execute(WebSocket webSocket)
    {
        
        var message = "Attack command executed with data: " + Command;
        var messageBytes = Encoding.UTF8.GetBytes(message);
        await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }
}