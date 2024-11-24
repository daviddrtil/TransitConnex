namespace TransitConnex.Command.Commands.User;

public class UserDeleteCommand : IUserCommand
{
    public Guid Id { get; set; }
}
