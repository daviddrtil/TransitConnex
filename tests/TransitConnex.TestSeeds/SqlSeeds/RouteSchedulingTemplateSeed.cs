using TransitConnex.Command.Data;
using TransitConnex.Domain.Models;

namespace TransitConnex.TestSeeds.SqlSeeds;

public class RouteSchedulingTemplateSeed
{
    public static readonly List<RouteSchedulingTemplate> RouteSchedulingTemplates = [
        new()
        {
            Id = Guid.Parse("689098c4-5b0b-45b3-8425-79438b57fd6e"),
            Name = "R8 Brno-Přerov - main",
            RouteId = Guid.Parse("9d8c5c9f-ca03-4399-96ff-8f081b67d298"),
            Template = "{\n    \"scheduledRoutes\": [\n        {\n            \"vehicleId\": \"687bdc2c-46c6-4dc0-b4fc-42bde1bd1006\",\n            \"startTime\": \"12:00:00\"\n        },\n        {\n            \"vehicleId\": \"9d007ab8-dd0d-48bc-9756-ec5b3760ecb7\",\n            \"startTime\": \"13:00:00\"\n        },\n        {\n            \"vehicleId\": \"687bdc2c-46c6-4dc0-b4fc-42bde1bd1006\",\n            \"startTime\": \"14:00:00\"\n        },\n        {\n            \"vehicleId\": \"9d007ab8-dd0d-48bc-9756-ec5b3760ecb7\",\n            \"startTime\": \"15:00:00\"\n        },\n        {\n            \"vehicleId\": \"687bdc2c-46c6-4dc0-b4fc-42bde1bd1006\",\n            \"startTime\": \"16:00:00\"\n        },\n        {\n            \"vehicleId\": \"9d007ab8-dd0d-48bc-9756-ec5b3760ecb7\",\n            \"startTime\": \"17:00:00\"\n        }\n    ]\n}"
        },
        new()
        {
            Id = Guid.Parse("73d0bf97-d7ac-43b4-9b95-31dd81a986c4"),
            Name = "R8 Přerov-Brno - main",
            RouteId = Guid.Parse("dff25738-54e3-4190-b19d-282a300c8219"),
            Template = "{\n    \"scheduledRoutes\": [\n        {\n            \"vehicleId\": \"687bdc2c-46c6-4dc0-b4fc-42bde1bd1006\",\n            \"startTime\": \"11:00:00\",\n            \"price\": 100        \n        },\n        {\n            \"vehicleId\": \"9d007ab8-dd0d-48bc-9756-ec5b3760ecb7\",\n            \"startTime\": \"12:00:00\",\n            \"price\": 100  \n        },\n        {\n            \"vehicleId\": \"687bdc2c-46c6-4dc0-b4fc-42bde1bd1006\",\n            \"startTime\": \"13:00:00\",\n            \"price\": 100 \n        },\n        {\n            \"vehicleId\": \"9d007ab8-dd0d-48bc-9756-ec5b3760ecb7\",\n            \"startTime\": \"14:00:00\",\n            \"price\": 100 \n        },\n        {\n            \"vehicleId\": \"687bdc2c-46c6-4dc0-b4fc-42bde1bd1006\",\n            \"startTime\": \"15:00:00\",\n            \"price\": 120 \n        },\n        {\n            \"vehicleId\": \"9d007ab8-dd0d-48bc-9756-ec5b3760ecb7\",\n            \"startTime\": \"16:00:00\",\n            \"price\": 120 \n        }\n    ]\n}"
        },
        new()
        {
            Id = Guid.Parse("a1ade26f-5e07-43c6-b9b9-d14ae6d8ed26"),
            Name = "BRN-32 Srbská-Česká - main",
            RouteId = Guid.Parse("f83d0060-c5ad-4e68-ac4d-8e9f1e7364e2"),
            Template = "{\n    \"scheduledRoutes\": [\n        {\n            \"vehicleId\": \"caf27030-e49a-404c-ad1b-4bc4e15c094a\",\n            \"startTime\": \"12:39:00\"\n        },\n        {\n            \"vehicleId\": \"1b23b003-4fd9-487e-9523-ed2ca383d66f\",\n            \"startTime\": \"12:54:00\"\n        },\n        {\n            \"vehicleId\": \"caf27030-e49a-404c-ad1b-4bc4e15c094a\",\n            \"startTime\": \"13:09:00\"\n        },\n        {\n            \"vehicleId\": \"1b23b003-4fd9-487e-9523-ed2ca383d66f\",\n            \"startTime\": \"13:24:00\"\n        },\n        {\n            \"vehicleId\": \"caf27030-e49a-404c-ad1b-4bc4e15c094a\",\n            \"startTime\": \"13:39:00\"\n        },\n        {\n            \"vehicleId\": \"1b23b003-4fd9-487e-9523-ed2ca383d66f\",\n            \"startTime\": \"13:52:00\"\n        }\n    ]\n}"
        },
        new()
        {
            Id = Guid.Parse("f7e04be9-32bb-44dd-8967-fb4ea58a5ceb"),
            Name = "BRN-32 Česká-Srbská - main",
            RouteId = Guid.Parse("829c534e-ae42-48f9-b66f-3f5521c522c3"),
            Template = "{\n    \"scheduledRoutes\": [\n        {\n            \"vehicleId\": \"caf27030-e49a-404c-ad1b-4bc4e15c094a\",\n            \"startTime\": \"12:22:00\"     \n        },\n        {\n            \"vehicleId\": \"1b23b003-4fd9-487e-9523-ed2ca383d66f\",\n            \"startTime\": \"12:37:00\"\n        },\n        {\n            \"vehicleId\": \"caf27030-e49a-404c-ad1b-4bc4e15c094a\",\n            \"startTime\": \"12:52:00\"\n        },\n        {\n            \"vehicleId\": \"1b23b003-4fd9-487e-9523-ed2ca383d66f\",\n            \"startTime\": \"13:07:00\"\n        },\n        {\n            \"vehicleId\": \"caf27030-e49a-404c-ad1b-4bc4e15c094a\",\n            \"startTime\": \"13:22:00\"\n        },\n        {\n            \"vehicleId\": \"1b23b003-4fd9-487e-9523-ed2ca383d66f\",\n            \"startTime\": \"13:37:00\"\n        }\n    ]\n}"
        }
    ];

    public static void Seed(AppDbContext context)
    {
        foreach (var routeSchedulingTemplate in RouteSchedulingTemplates)
        {
            context.RouteSchedulingTemplates.Add(routeSchedulingTemplate);
        }
        context.SaveChanges();
    }
}
