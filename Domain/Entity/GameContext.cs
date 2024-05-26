using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Domain.Entity;

public class GameContext
{
    public Guid GameId { get; set; }
    public List<Player> Players { get; set; }
    public string GameState { get; set; } = "StateGame";
    private readonly TimeSpan _tickInterval = TimeSpan.FromMilliseconds(1000 / 30);
    private readonly TimeSpan _gameDuration;
    private int Rows { get; set; }
    private int Columns { get; set; } 
    private CancellationTokenSource _cts;
    public GameContext()
    {
        Players = new List<Player>();
        Rows = 800;
        Columns = 800;
    }

    public void AddPlayer(Player player)
    {
        Players.Add(player);
    }

    public void UpdatePlayerPosition(Guid playerId, int x, int y)
    {
        var player = Players.FirstOrDefault(p => p.Id == playerId);
        if (player != null)
        {
            player.X = x;
            player.Y = y;
        }
    }
    
   
        public void StartGameLoop()
        {
            _cts = new CancellationTokenSource();
        
            Task.Run(async () =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    var start = DateTime.UtcNow;

                 
                    UpdateGameState();

                    var end = DateTime.UtcNow;
                    var elapsed = end - start;
                    var delay = _tickInterval - elapsed;

                    if (delay > TimeSpan.Zero)
                    {
                        await Task.Delay(delay, _cts.Token);
                    }
                }
            }, _cts.Token);
        }

        public void StopGameLoop()
        {
            _cts?.Cancel();
        }
       
        private void UpdateGameState()
        {
            foreach (var player in Players)
            {
                Console.WriteLine($"Updating game state Player:{player.Id}");
                if(player.MoveDirection == MoveDirection.Up && player.Y > 0)
                {
                    player.Y -= 1;
                }
                else if(player.MoveDirection == MoveDirection.Down && player.Y < Columns - 1)
                {
                    player.Y += 1;
                }
                else if(player.MoveDirection == MoveDirection.Left && player.X > 0)
                {
                    player.X -= 1;
                }
                else if(player.MoveDirection == MoveDirection.Right && player.X <  Rows- 1)
                {
                    player.X += 1;
                }

                player.WebSocket.SendAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this)), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }