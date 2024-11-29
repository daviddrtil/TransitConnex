using System.Net;
using System.Net.Http.Json;
using TransitConnex.API;
using TransitConnex.Command.Commands.Location;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Domain.Enums;
using TransitConnex.Tests.Infrastructure;
using Xunit.Abstractions;
using TransitConnex.TestSeeds.SqlSeeds;

namespace TransitConnex.Tests;

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
    public async Task POST_Create_Location_And_Get_is_OK()
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

    //[Fact]
    //public async Task POST_Get_Closest_Location_OK()
    //{
        
    //}
}
