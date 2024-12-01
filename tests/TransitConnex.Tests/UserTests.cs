using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using TransitConnex.API;
using TransitConnex.Domain.DTOs;
using TransitConnex.Domain.DTOs.ScheduledRoute;
using TransitConnex.Query.Queries;
using TransitConnex.Tests.Infrastructure;
using TransitConnex.TestSeeds.NoSqlSeeds;
using TransitConnex.TestSeeds.SqlSeeds;
using Xunit.Abstractions;

namespace TransitConnex.Tests;

[Collection("NonParallelTests")]
public class UserTests(
    ITestOutputHelper testOutputHelper,
    ApiWebApplicationFactory<Program> fixture
) : APITestsBase(testOutputHelper, fixture)
{
    private const string Endpoint = "/api";

    [Fact]
    public async Task GET_User_Favourite_Locations_is_OK()
    {
        // Arrange
        await PerformLogin(UserSeed.BasicLogin);
        var dbFavLoc = LocationSeed.UserFavLocationBrno;
        string url = $"{Endpoint}/User/GetFavouriteLocations";

        // Act
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var favLocs = await response.Content.ReadFromJsonAsync<IEnumerable<UserFavLocationDto>>();

        // Assert
        Assert.NotNull(favLocs);
        Assert.NotEmpty(favLocs);

        var firstFavLoc = favLocs.First();
        Assert.Equal(dbFavLoc.LocationId, firstFavLoc.LocationId);
    }

    [Fact]
    public async Task GET_User_Favourite_Connections_is_OK()
    {
        // Arrange
        await PerformLogin(UserSeed.BasicLogin);
        var dbFavConn = LocationSeed.UserFavConnectionBrnoPrerov;
        string url = $"{Endpoint}/User/GetFavouriteConnections";

        // Act
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var favLocs = await response.Content.ReadFromJsonAsync<IEnumerable<UserFavConnectionDto>>();

        // Assert
        Assert.NotNull(favLocs);
        Assert.NotEmpty(favLocs);

        var firstFavLoc = favLocs.First();
        Assert.Equal(dbFavConn.FromLocationId, firstFavLoc.FromLocationId);
        Assert.Equal(dbFavConn.ToLocationId, firstFavLoc.ToLocationId);
    }

    [Fact]
    public async Task GET_User_SearchedRoutes_is_OK()
    {
        // Arrange
        await PerformLogin(UserSeed.BasicLogin);
        var firstDbRoute = SearchedRouteDocSeeder.SearchedRoutes.First();
        string url = $"{Endpoint}/User/GetSearchedRoutes";

        // Act
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var searchedRoutes = await response.Content.ReadFromJsonAsync<IEnumerable<SearchedRouteDto>>();

        // Assert
        Assert.NotNull(searchedRoutes);
        Assert.NotEmpty(searchedRoutes);

        var ids = searchedRoutes.Select(x => x.Id);
        var firstRoute = searchedRoutes.First();
        Assert.Contains(firstRoute.Id, ids);

        var dbRoute = searchedRoutes.First(x => x.Id == firstRoute.Id);
        Assert.Equal(dbRoute.FromLocationId, firstRoute.FromLocationId);
        Assert.Equal(dbRoute.ToLocationId, firstRoute.ToLocationId);
    }
}
