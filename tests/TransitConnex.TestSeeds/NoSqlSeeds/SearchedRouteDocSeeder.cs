using Bogus;
using TransitConnex.Domain.Collections;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;
using TransitConnex.TestSeeds.SqlSeeds;

namespace TransitConnex.TestSeeds.NoSqlSeeds;

public class SearchedRouteDocSeeder(
    Faker faker,
    IScheduledRouteMongoService scheduledRouteMongoService,
    ISearchedRouteMongoRepository searchedRouteRepo)
{
    public async Task Seed()
    {
        var srDocs = new List<SearchedRouteDoc>();
        for (int i = 0; i < 5; i++)
        {
            var fromLocation = faker.Random.CollectionItem(LocationSeed.Locations).Id;
            var toLocation = faker.Random.CollectionItem(LocationSeed.Locations).Id;
            var searchTime = DateTime.Parse("2024-10-14");
            var scheduledRoutes = await scheduledRouteMongoService.GetScheduledRoutes(
                fromLocation, toLocation, searchTime);
            var scheduledRouteIds = scheduledRoutes.Select(sr => sr.Id).ToList();
            var srDoc = new SearchedRouteDoc
            {
                Id = Guid.NewGuid(),
                UserId = UserSeed.BasicUser.Id,
                FromLocation = fromLocation,
                ToLocation = toLocation,
                Time = searchTime,
                ScheduledRouteIds = scheduledRouteIds,
            };
            srDocs.Add(srDoc);
        }
        await searchedRouteRepo.Upsert(srDocs);
    }
}
