using System.Text.Json.Serialization;

namespace TransitConnex.Command.Commands.User;

public class UserLikeLocationCommand : IUserCommand
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    public required Guid LocationId { get; set; }
}
