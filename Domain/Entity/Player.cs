using System.Net.WebSockets;

namespace Domain.Entity;

public class Player
{
    public Guid Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    
    public WebSocket WebSocket { get; set; }
    public MoveDirection MoveDirection { get; set; }
}