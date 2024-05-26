using System.Net.WebSockets;

namespace Domain.Interfaces;

public interface ICommand
{
    Task Execute(WebSocket webSocket);
}