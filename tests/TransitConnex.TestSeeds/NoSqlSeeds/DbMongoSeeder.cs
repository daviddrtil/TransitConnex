namespace TransitConnex.TestSeeds.NoSqlSeeds;

public class DbMongoSeeder(
    LocationDocSeeder locationSeeder,
    ScheduledRouteDocSeeder scheduledRouteSeeder,
    UserFavDocSeeder userFavDocSeeder,
    VehicleDocSeeder vehicleSeeder,
    VehicleRTIDocSeeder vehicleRTISeeder,
    SearchedRouteDocSeeder searchedRouteSeeder)
{
    public async Task SeedAll()
    {
        // Seed from sql database
        await locationSeeder.Seed();
        await vehicleSeeder.Seed();
        await scheduledRouteSeeder.Seed();
        await vehicleRTISeeder.Seed();
        await userFavDocSeeder.Seed();
        await searchedRouteSeeder.Seed();

        // Previous seeds from dataset - Must be seeded in order
        //await vehicleRTISeeder.Seed();
        //await scheduledRouteSeeder.Seed();
        //await locationSeeder.Seed();
        //await searchedRouteSeeder.Seed();
    }
}
