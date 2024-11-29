using Bogus;
using TransitConnex.Command.Seeds;
using TransitConnex.Domain.Collections;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Seeds;

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
            Guid fromLocation = faker.Random.CollectionItem(LocationSeed.Locations).Id;
            Guid toLocation = faker.Random.CollectionItem(LocationSeed.Locations).Id;
            DateTime searchTime = DateTime.Parse("2024-10-14");
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
