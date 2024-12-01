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
    public static List<SearchedRouteDoc> SearchedRoutes = [];
    public async Task Seed()
    {
        SearchedRoutes = [];
        var searchTime = DateTime.Parse("2024-08-14");
        for (int i = 0; i < 5; i++)
        {
            var fromLocation = faker.Random.CollectionItem(LocationSeed.Locations).Id;
            var toLocation = faker.Random.CollectionItem(LocationSeed.Locations).Id;
            searchTime = searchTime.AddDays(7);
            var scheduledRoutes = await scheduledRouteMongoService.GetScheduledRoutes(
                fromLocation, toLocation, searchTime);
            var scheduledRouteIds = scheduledRoutes.Select(sr => sr.Id).ToList();
            var srDoc = new SearchedRouteDoc
            {
                Id = Guid.NewGuid(),
                UserId = UserSeed.BasicUser.Id,
                FromLocationId = fromLocation,
                ToLocationId = toLocation,
                SearchTime = searchTime,
                ScheduledRouteIds = scheduledRouteIds,
            };
            SearchedRoutes.Add(srDoc);
        }
        await searchedRouteRepo.Upsert(SearchedRoutes);
    }
}
