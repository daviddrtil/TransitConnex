using System.Net;
using System.Net.Http.Json;
using TransitConnex.API;
using TransitConnex.Command.Commands.Icon;
using TransitConnex.Domain.DTOs.Icon;
using TransitConnex.Query.Queries;
using TransitConnex.Tests.Infrastructure;
using TransitConnex.TestSeeds.SqlSeeds;
using Xunit.Abstractions;

namespace TransitConnex.Tests;

public class IconTests(
    ITestOutputHelper testOutputHelper,
    ApiWebApplicationFactory<Program> fixture)
    : APITestsBase(testOutputHelper, fixture)
{
    private const string Endpoint = "/api/Icon";
    
    [Fact]
    public async Task POST_Create_Icon_And_GetFiltered_is_OK()
    {
        await PerformLogin(UserSeed.AdminLogin);

        var response = await Client.PostAsJsonAsync($"{Endpoint}",
            new IconCreateCommand {Name = "test icon", Svg = "some svg"});
        response.EnsureSuccessStatusCode();
        Guid id = await response.Content.ReadFromJsonAsync<Guid>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        // Call get filtered
        var filterResponse = await Client.PostAsJsonAsync($"{Endpoint}/filter", new IconFilteredQuery {Ids = [id]});
        filterResponse.EnsureSuccessStatusCode();
        var iconList = await filterResponse.Content.ReadFromJsonAsync<List<IconDto>>();
        
        Assert.NotNull(iconList);
        Assert.NotEmpty(iconList);

        var firstIcon = iconList.First();
        Assert.Equal(id, firstIcon.Id);
        Assert.Equal(firstIcon.Name, firstIcon.Name);
    }
}
