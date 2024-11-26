namespace TransitConnex.Command.Commands.User;

public class UserLikeLocationCommand : IUserCommand
{
    public required Guid UserId { get; set; }
    public required Guid LocationId { get; set; }
}
