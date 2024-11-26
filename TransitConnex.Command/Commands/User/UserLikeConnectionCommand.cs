namespace TransitConnex.Command.Commands.User;

public class UserLikeConnectionCommand : IUserCommand
{
    public required Guid UserId { get; set; }
    public required Guid FromLocationId { get; set; }
    public required Guid ToLocationId { get; set; }
    // public required Guid LineId { get; set; }
}
