using TransitConnex.API;
using TransitConnex.Tests.Infrastructure;
using Xunit.Abstractions;

namespace TransitConnex.Tests;

public class ScheduledRouteTests(
    ITestOutputHelper testOutputHelper,
    ApiWebApplicationFactory<Program> fixture
) : APITestsBase(testOutputHelper, fixture)
{
    private const string Endpoint = "/api";

    //[Fact]
    //public async Task GET_Scheduled_Routes_is_OK()
    //{
    //    // todo get actual values
    //    var startLocationId = Guid.NewGuid();
    //    var endLocationId = Guid.NewGuid();
    //    var startTime = DateTime.Parse("2024-11-01 12:00:00");
    //    var scheduledRouteId = Guid.Parse("00000000-0000-0000-0000-000000000000");

    //    // Arrange
    //    await PerformLogin(UserSeeds.BasicLogin);
    //    var queryParams = new Dictionary<string, string?>
    //    {
    //        { "startLocationId", startLocationId.ToString() },
    //        { "endLocationId", endLocationId.ToString() },
    //        { "startTime", startTime.ToString() },
    //    };
    //    string url = QueryHelpers.AddQueryString($"{Endpoint}/ScheduledRoute/GetScheduledRoutes", queryParams);

    //    // Act
    //    var response = await Client.GetAsync(url);
    //    response.EnsureSuccessStatusCode();
    //    var scheduledRoutes = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduledRouteDto>>();

    //    // Assert
    //    Assert.NotNull(scheduledRoutes);
    //    Assert.Empty(scheduledRoutes);

    //    //Assert.NotEmpty(scheduledRoutes);
    //    //Assert.Equal(scheduledRouteId, scheduledRoutes.First().Id);
    //}
}
