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

namespace TransitConnex.Tests;

[Collection("NonParallelTests")]
public class LocationTests(
    ITestOutputHelper testOutputHelper,
    ApiWebApplicationFactory<Program> fixture)
        : APITestsBase(testOutputHelper, fixture)
{
    private const string Endpoint = "/api";

    /// <summary>
    ///     Tests create of a location to sql and synchronization to nosql MongoDb,
    ///     and get of the created location
    /// </summary>
    [Fact]
    public async Task POST_Create_Location_And_GetById_OK()
    {
        await PerformLogin(UserSeed.AdminLogin);
        var newLocation = new LocationCreateCommand()
        {
            Name = "Praha",
            Longitude = 14.418540,
            Latitude = 50.073658,
            Type = LocationTypeEnum.CITY,
        };

        string url = $"{Endpoint}/Location";
        var response = await Client.PostAsJsonAsync(url, newLocation);
        response.EnsureSuccessStatusCode();
        Guid id = await response.Content.ReadFromJsonAsync<Guid>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        // Test GET of the created location
        string getUrl = $"{Endpoint}/Location/GetById/{id}";
        var getResponse = await Client.GetAsync(getUrl);
        getResponse.EnsureSuccessStatusCode();
        var getLocation = await getResponse.Content.ReadFromJsonAsync<LocationDto>();

        Assert.NotNull(getLocation);
        Assert.Equal(id, getLocation.Id);
        Assert.Equal(newLocation.Name, getLocation.Name);
    }

    [Fact]
    public async Task GET_ByName_Location_OK()
    {
        // Arrange
        await PerformLogin(UserSeed.BasicLogin);
        var dbLocation = LocationSeed.BrnoCityLocation;
        var queryParams = new Dictionary<string, string?>
        {
            { "name", dbLocation.Name },
        };
        string url = QueryHelpers.AddQueryString($"{Endpoint}/Location/GetByname", queryParams);

        // Act
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var locations = await response.Content.ReadFromJsonAsync<IEnumerable<LocationDto>>();

        // Assert
        Assert.NotNull(locations);
        Assert.NotEmpty(locations);

        var firstLocation = locations.First();
        Assert.Equal(dbLocation.Id, firstLocation.Id);
        Assert.Equal(dbLocation.Name, firstLocation.Name);
        Assert.Equal(dbLocation.Longitude, firstLocation.Longitude);
        Assert.Equal(dbLocation.Latitude, firstLocation.Latitude);
    }

    [Fact]
    public async Task GET_Closest_Location_OK()
    {
        // Arrange
        await PerformLogin(UserSeed.BasicLogin);
        var dbLocation = LocationSeed.BrnoCityLocation;
        var queryParams = new Dictionary<string, string?>
        {
            { "longitude", dbLocation.Longitude.ToString() },
            { "latitude", dbLocation.Latitude.ToString() },
        };
        string url = QueryHelpers.AddQueryString($"{Endpoint}/Location/GetClosest", queryParams);

        // Act
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var location = await response.Content.ReadFromJsonAsync<LocationDto?>();

        // Assert
        Assert.NotNull(location);
        Assert.Equal(dbLocation.Id, location.Id);
        Assert.Equal(dbLocation.Name, location.Name);
        Assert.Equal(dbLocation.Longitude, location.Longitude);
        Assert.Equal(dbLocation.Latitude, location.Latitude);
    }
}
