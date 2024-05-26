namespace Domain.Entity;

public class CommandMove: CommandBase
{
    public CommandMove(Guid playerId, Guid gameId, MoveDirection direction) : base( playerId, gameId)
    {
        Direction = direction;
    }
    public MoveDirection Direction { get; set; }
};