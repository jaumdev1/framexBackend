using Application.Services;
using Domain.Entity;
using Domain.ModelRequest;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public static class GameRoutes
{
  

    public static void MapGameRoutes(this WebApplication app)
    {
        var gameService = app.Services.GetRequiredService<GameService>();

        app.MapPost("/createGame", () =>
        {
            var game = gameService.CreateGame();
            var player = new Player { Id = Guid.NewGuid()};
            game.Players.Add(player);
            return Results.Ok(new { GameId = game.GameId, PlayerId = player.Id });
        });
        app.MapPost("/startGame", (JoinGameRequest request) =>
        {
            var game = gameService._games[request.GameId];
            game.StartGameLoop();
            return Results.Ok($"Game {game.GameId} started");
        });
        app.MapPost("/joinGame", (JoinGameRequest request) =>
        {
            var game = gameService._games[request.GameId];
            var player = new Player { Id = Guid.NewGuid()};
            game.Players.Add(player);
            return Results.Ok(new { GameId = game.GameId, PlayerId = player.Id });
        });
    }
}

