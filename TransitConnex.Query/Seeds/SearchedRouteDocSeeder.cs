using Bogus;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Seeds;

public class SearchedRouteDocSeeder(
    Faker faker,
    ISearchedRouteMongoRepository searchedRouteRepo)
{
    public async Task Seed()
    {
        var scheduledRouteIds = ScheduledRouteDocSeeder.ScheduledRoutes
            .Select(sr => sr.Id).ToList();
        var searchedRoutes = new List<SearchedRouteDoc>();
        for (int i = 0; i < 1000; i++)
        {
            var searchedRoute = new SearchedRouteDoc
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                FromLocation = faker.Random.CollectionItem(LocationDocSeeder.Locations).Name,
                ToLocation = faker.Random.CollectionItem(LocationDocSeeder.Locations).Name,
                Time = faker.Date.Past(),
                ScheduledRouteIds = faker.Random.ListItems(scheduledRouteIds, 5)
            };
            searchedRoutes.Add(searchedRoute);
        }
        await searchedRouteRepo.Upsert(searchedRoutes);
    }
}
