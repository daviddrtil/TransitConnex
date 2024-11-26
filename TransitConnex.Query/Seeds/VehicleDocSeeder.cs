using Bogus;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query.Seeds;

public class VehicleDocSeeder(
    Faker faker,
    IVehicleMongoRepository vehicleRepo)
{
    public async Task Seed()
    {

    }
}
