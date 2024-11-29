using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Data;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.TestSeeds.NoSqlSeeds;

public class UserFavDocSeeder(
    AppDbContext context,
    IUserFavConnectionMongoService userFavConnectionService,
    IUserFavLocationMongoService userFavLocationService)
{
    public async Task Seed()
    {
        var userFavLocations = await context.UserLocationFavourites.ToListAsync();
        foreach (var userFavLocation in userFavLocations)
        {
            await userFavLocationService.Add(userFavLocation);
        }

        var userFavConnections = await context.UserConnectionFavourites.ToListAsync();
        foreach (var userFavConnection in userFavConnections)
        {
            await userFavConnectionService.Add(userFavConnection);
        }
    }
}
