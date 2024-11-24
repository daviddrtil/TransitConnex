using Bogus;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using TransitConnex.Domain.Collections;
using TransitConnex.Domain.Collections.NestedDocuments;

namespace CollectionGenerator;

internal class VehicleParser
{
    private static readonly Faker Faker = new("cz");

    private static VehicleRTIDoc Parse(CsvReader csv)
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
            Coordinates =
                new Coordinate
                {
                    Latitude = double.Parse(csv.GetField<string>("lat") ?? "0"),
                    Longitude = double.Parse(csv.GetField<string>("lng") ?? "0")
                },
            Speed = double.Round(Faker.Random.Double(0.0, 60.0), 1),
            Temperature = double.Round(Faker.Random.Double(15.0, 25.0), 2),
            Occupancy = Faker.Random.Int(0, 200),
            Delay = int.Parse(csv.GetField<string>("delay") ?? "0"),
            IsInactive = bool.Parse(csv.GetField<string>("isinactive") ?? false.ToString()),
            IsStuck = Faker.Random.Bool(0.01f),
            LineId = csv.GetGuid("lineid"),
            ScheduledRouteId = csv.GetGuid("routeid"),
            LastStopId = csv.GetGuid("laststopid"),
            FinalStopId = csv.GetGuid("finalstopid")
        };
        return vehicle;
    }

    public static List<VehicleRTIDoc> LoadVehicleRTIs()
    {
        var vehicleRTIs = new List<VehicleRTIDoc>();
        string datasetPath = Path.Combine(Program.ProjectPath, "Datasets", "vehicleDataset.csv");
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HeaderValidated = null,
            MissingFieldFound = null
        };
        using var reader = new StreamReader(datasetPath);
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
}
