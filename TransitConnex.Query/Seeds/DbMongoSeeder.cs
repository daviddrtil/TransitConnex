namespace TransitConnex.Query.Seeds;

public class DbMongoSeeder(
    LocationDocSeeder locationSeeder,
    RouteStopDocSeeder routeStopSeeder,
    ScheduledRouteDocSeeder scheduledRouteSeeder,
    SearchedRouteDocSeeder searchedRouteSeeder,
    VehicleDocSeeder vehicleSeeder,
    VehicleRTIDocSeeder vehicleRTISeeder)
{
    public async Task SeedAll()
    {
        // Must be seeded in order
        await vehicleRTISeeder.Seed();
        await routeStopSeeder.Seed();
        await scheduledRouteSeeder.Seed();
        await locationSeeder.Seed();
        await searchedRouteSeeder.Seed();
        await vehicleSeeder.Seed();
    }
}
