using Domain.Entity;

namespace Application.Services;

public class GameService
{
    public readonly Dictionary<Guid, GameContext> _games;
    public static GameService Instance { get; private set; }
    public GameService()
    {
        _games = new Dictionary<Guid, GameContext>();
        Instance = this;
    }

    public GameContext CreateGame()
    {
        var game = new GameContext { GameId = Guid.NewGuid() };
        _games.Add(game.GameId, game);
        return game;
    }
    public GameContext GetGame(Guid gameId)
    {
        _games.TryGetValue(gameId, out var game);
        return game;
    }
    
}