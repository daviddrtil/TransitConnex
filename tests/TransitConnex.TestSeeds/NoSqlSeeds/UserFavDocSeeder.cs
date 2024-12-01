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
        var userFavLocations = await context.UserLocationFavourites
            .AsNoTracking()
            .ToListAsync();
        foreach (var userFavLocation in userFavLocations)
        {
            var dbUserFavLocations = await userFavLocationService.GetByUserId(userFavLocation.UserId);
            if (!dbUserFavLocations.Any(x =>
                x.UserId == userFavLocation.UserId
                && x.LocationId == userFavLocation.LocationId))
                await userFavLocationService.Add(userFavLocation);
        }

        var userFavConnections = await context.UserConnectionFavourites
            .AsNoTracking()
            .ToListAsync();
        foreach (var userFavConnection in userFavConnections)
        {
            var dbUserFavConnections = await userFavConnectionService.GetByUserId(userFavConnection.UserId);
            if (!dbUserFavConnections.Any(x =>
                x.UserId == userFavConnection.UserId
                && x.FromLocationId == userFavConnection.FromLocationId
                && x.ToLocationId == userFavConnection.ToLocationId))
                await userFavConnectionService.Add(userFavConnection);
        }
    }
}
