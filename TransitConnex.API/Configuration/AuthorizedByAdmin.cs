using Microsoft.AspNetCore.Authorization;

namespace TransitConnex.API.Configuration;

public class AuthorizedByAdmin : AuthorizeAttribute
{
    private const string AdminRole = "Admin";
    public AuthorizedByAdmin()
    {
        Roles = AdminRole;
    }
}
