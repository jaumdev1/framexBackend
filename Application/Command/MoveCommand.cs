

using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Services;
using Domain.Entity;
using Domain.Interfaces;

namespace Application.Command;


public class MoveCommand : ICommand
{   
    public CommandMove Command { get; set; }

    public MoveCommand(string data)
    {
        Command = JsonSerializer.Deserialize<CommandMove>(data);
    }

 
    public async Task Execute(WebSocket webSocket)
    {    var gameService = GameService.Instance;
        var message = "{}" ;
        var messageBytes = Encoding.UTF8.GetBytes(message);
        gameService._games[Command.GameId].Players.Where(p => p.Id == Command.PlayerId).First().MoveDirection = Command.Direction;
        await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }
}