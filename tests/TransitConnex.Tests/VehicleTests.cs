using System.Net;
using System.Net.Http.Json;
using TransitConnex.API;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Domain.Enums;
using TransitConnex.Tests.Infrastructure;
using Xunit.Abstractions;
using TransitConnex.TestSeeds.SqlSeeds;
using Microsoft.AspNetCore.WebUtilities;
using TransitConnex.Domain.DTOs;
using TransitConnex.Domain.DTOs.Vehicle;

namespace TransitConnex.Tests;

[Collection("NonParallelTests")]
public class VehicleTests(
    ITestOutputHelper testOutputHelper,
    ApiWebApplicationFactory<Program> fixture)
        : APITestsBase(testOutputHelper, fixture)
{
    private const string Endpoint = "/api";

    [Fact]
    public async Task GET_VehicleRTI_By_VehicleId_OK()
    {
        // Arrange
        await PerformLogin(UserSeed.BasicLogin);
        var dbVehicle = VehicleSeed.Vehicles.First();
        string url = $"{Endpoint}/Vehicle/GetRTIByVehicleId/{dbVehicle.Id}";

        // Act
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var vehicleRTI = await response.Content.ReadFromJsonAsync<VehicleRTIDto>();

        // Assert
        Assert.NotNull(vehicleRTI);
        Assert.Equal(vehicleRTI.VehicleId, dbVehicle.Id);
    }

    [Fact]
    public async Task GET_Vehicle_By_Id_OK()
    {
        // Arrange
        await PerformLogin(UserSeed.BasicLogin);
        var dbVehicle = VehicleSeed.Vehicles.First();
        string url = $"{Endpoint}/Vehicle/{dbVehicle.Id}";

        // Act
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var vehicle = await response.Content.ReadFromJsonAsync<VehicleDto>();

        // Assert
        Assert.NotNull(vehicle);
        Assert.Equal(vehicle.Id, dbVehicle.Id);
        Assert.Equal(vehicle.Spz, dbVehicle.Spz);
    }
}
