namespace Domain.Entity;

public class CommandBase
{
    public CommandBase(Guid playerId, Guid gameId)
    {
       
        PlayerId = playerId;
        GameId = gameId;
    }

    public Guid PlayerId { get; set; }
    public Guid GameId { get; set; }
    
}