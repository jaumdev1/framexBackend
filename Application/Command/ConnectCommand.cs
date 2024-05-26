using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Application.Services;
using Domain.Entity;
using Domain.Interfaces;

namespace Application.Command;

public class ConnectCommand : ICommand
{
    public CommandBase Command { get; set; }


    public ConnectCommand(string data)
    {
        Command = JsonSerializer.Deserialize<CommandBase>(data);
  
    }

    public async Task Execute(WebSocket webSocket)
    {
        var gameService = GameService.Instance;
        var message = "Player Connected";
        var result = new
        {
            Command = "PlayerConnected",
            PlayerId = Command.PlayerId,
            GameId = Command.GameId
        };
        var messageJson = JsonSerializer.Serialize(result);
        var messageBytes = Encoding.UTF8.GetBytes(messageJson);
        gameService._games[Command.GameId].Players.Where(p => p.Id == Command.PlayerId).First().WebSocket = webSocket;
        await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }
}