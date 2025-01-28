using System.Text.Json.Serialization;

namespace TransitConnex.Command.Commands.User;

public class UserLikeConnectionCommand : IUserCommand
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    public required Guid FromLocationId { get; set; }
    public required Guid ToLocationId { get; set; }
}
