using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Data;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query.Seeds;

public class VehicleDocSeeder(
    AppDbContext context,
    IVehicleMongoService vehicleService)
{
    public static List<Guid> VehicleIds = [];
    public async Task Seed()
    {
        var vehicles = await context.Vehicles.ToListAsync();
        VehicleIds = vehicles.Select(v => v.Id).ToList();
        await vehicleService.Create(vehicles);
    }
}
