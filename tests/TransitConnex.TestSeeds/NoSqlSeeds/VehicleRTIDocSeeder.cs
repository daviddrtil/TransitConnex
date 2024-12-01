using Bogus;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using TransitConnex.Domain.Collections;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.TestSeeds.NoSqlSeeds.Common;

namespace TransitConnex.TestSeeds.NoSqlSeeds;

public class VehicleRTIDocSeeder(
    Faker faker,
    IVehicleRTIMongoRepository vehicleRTIRepo)
{
    public static readonly HashSet<Guid> VehicleIds = [];
    public static readonly HashSet<Guid> RouteIds = [];
    public static readonly HashSet<Guid> StopIds = [];

    public static string SolutionPath = ProjectPathHelper.GetSolutionDirectory();
    public static string ProjectPath { get; set; } = Path.Combine(
        SolutionPath, "tests", "TransitConnex.TestSeeds");
    public static string DatasetPath = Path.Combine(ProjectPath,
        "NoSqlSeeds", "Datasets", "vehicleDataset.csv");

    private VehicleRTIDoc Parse(CsvReader csv)
    {
        var vehicle = new VehicleRTIDoc
        {
            Id = Guid.NewGuid(),
            VehicleId = csv.GetGuid("id"),
            Updated = DateTime.Parse(
                csv.GetField<string>("lastupdate") ?? "0001/01/01 00:00:00.000+00",
                null,
                DateTimeStyles.RoundtripKind
            ),
            Latitude = double.Parse(csv.GetField<string>("lat") ?? "0"),
            Longitude = double.Parse(csv.GetField<string>("lng") ?? "0"),
            Speed = double.Round(faker.Random.Double(0.0, 60.0), 1),
            Temperature = double.Round(faker.Random.Double(15.0, 25.0), 2),
            Occupancy = faker.Random.Int(0, 200),
            Delay = int.Parse(csv.GetField<string>("delay") ?? "0"),
            IsInactive = bool.Parse(csv.GetField<string>("isinactive") ?? false.ToString()),
            IsStuck = faker.Random.Bool(0.01f),
            LineId = csv.GetGuid("lineid"),
            ScheduledRouteId = csv.GetGuid("routeid"),
            LastStopId = csv.GetGuid("laststopid"),
            FinalStopId = csv.GetGuid("finalstopid")
        };
        return vehicle;
    }

    public List<VehicleRTIDoc> LoadVehicleRTIs()
    {
        var vehicleRTIs = new List<VehicleRTIDoc>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HeaderValidated = null,
            MissingFieldFound = null
        };
        using var reader = new StreamReader(DatasetPath);
        using var csv = new CsvReader(reader, config);
        // Read header
        csv.Read();
        csv.ReadHeader();

        // Read records
        while (csv.Read())
        {
            var vehicleRTI = Parse(csv);
            vehicleRTIs.Add(vehicleRTI);
        }

        return vehicleRTIs;
    }

    public static void ModifyVehicleRTIs(List<VehicleRTIDoc> vehicleRTIs)
    {
        for (int i = 0; i < VehicleDocSeeder.VehicleIds.Count; i++)
        {
            var sqlVehicleId = VehicleDocSeeder.VehicleIds[i];
            var datasetVehicleId = vehicleRTIs[i].VehicleId;
            foreach (var vehicleRTI in vehicleRTIs)
            {
                if (vehicleRTI.VehicleId == datasetVehicleId)
                    vehicleRTI.VehicleId = sqlVehicleId;
            }
        }
    }

    public async Task Seed()
    {
        var vehicleRTIs = LoadVehicleRTIs();
        ModifyVehicleRTIs(vehicleRTIs);
        vehicleRTIs = vehicleRTIs.Take(100).ToList();
        var dbVehiclerRTIs = await vehicleRTIRepo.GetAll();
        if (dbVehiclerRTIs.Any())
            return;
        await vehicleRTIRepo.Upsert(vehicleRTIs);

        foreach (var v in vehicleRTIs)
        {
            VehicleIds.Add(v.VehicleId);
            RouteIds.Add(v.ScheduledRouteId);
            StopIds.Add(v.LastStopId);
        }
    }
}
