using TransitConnex.Command.Data;
using TransitConnex.Domain.Models;

namespace TransitConnex.TestSeeds.SqlSeeds;

public class IconSeed
{
    public static readonly Icon BusIcon = new()
    {
        Id = Guid.Parse("f9adf376-6827-45b3-91f5-6a6bf5241384"),
        Name = "Bus",
        Svg = "PHN2ZyB3aWR0aD0iNjQiIGhlaWdodD0iNjQiIHZpZXdCb3g9IjAgMCA2NCA2NCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB4PSI4IiB5PSIxNiIgd2lkdGg9IjQ4IiBoZWlnaHQ9IjMyIiByeD0iNCIgcnk9IjQiIGZpbGw9IiMwMDAiLz48Y2lyY2xlIGN4PSIyMCIgY3k9IjUyIiByPSI0IiBmaWxsPSIjMDAwIi8+PGNpcmNsZSBjeD0iNDQiIGN5PSI1MiIgcj0iNCIgZmlsbD0iIzAwMCIvPjxyZWN0IHg9IjE2IiB5PSIyMCIgd2lkdGg9IjMyIiBoZWlnaHQ9IjIwIiBmaWxsPSIjZmZmIi8+PC9zdmc+"
    };

    public static readonly Icon TramIcon = new()
    {
        Id = Guid.Parse("33a72711-fd16-4054-866f-050fc68f5b30"),
        Name = "Tram",
        Svg = "PHN2ZyB3aWR0aD0iNjQiIGhlaWdodD0iNjQiIHZpZXdCb3g9IjAgMCA2NCA2NCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB4PSIxMCIgeT0iMjAiIHdpZHRoPSI0NCIgaGVpZ2h0PSIyNCIgcng9IjQiIHJ5PSI0IiBmaWxsPSIjMDAwIi8+PGNpcmNsZSBjeD0iMjAiIGN5PSI1MCIgcj0iNCIgZmlsbD0iIzAwMCIvPjxjaXJjbGUgY3g9IjQ0IiBjeT0iNTAiIHI9IjQiIGZpbGw9IiMwMDAiLz48cmVjdCB4PSIxOCIgeT0iMjQiIHdpZHRoPSIyOCIgaGVpZ2h0PSIxNiIgZmlsbD0iI2ZmZiIvPjwvc3ZnPg=="
    };

    public static readonly Icon TrainIcon = new()
    {
        Id = Guid.Parse("0b44f285-e9ea-4bd1-94b6-cc42dc21e0a5"),
        Name = "Train",
        Svg = "PHN2ZyB3aWR0aD0iNjQiIGhlaWdodD0iNjQiIHZpZXdCb3g9IjAgMCA2NCA2NCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB4PSIxMiIgeT0iMTIiIHdpZHRoPSI0MCIgaGVpZ2h0PSI0MCIgcng9IjQiIHJ5PSI0IiBmaWxsPSIjMDAwIi8+PGNpcmNsZSBjeD0iMjAiIGN5PSI1NCIgcj0iNCIgZmlsbD0iIzAwMCIvPjxjaXJjbGUgY3g9IjQ0IiBjeT0iNTQiIHI9IjQiIGZpbGw9IiMwMDAiLz48cmVjdCB4PSIxOCIgeT0iMTgiIHdpZHRoPSIyOCIgaGVpZ2h0PSIyNCIgZmlsbD0iI2ZmZiIvPjwvc3ZnPg=="
    };

    public static readonly List<Icon> Icons = [
        BusIcon,
        TramIcon,
        TrainIcon,
    ];

    public static void Seed(AppDbContext context)
    {
        foreach (var icon in Icons)
        {
            if (!context.Icons.Any(i => i.Id == icon.Id))
            {
                context.Icons.Add(icon);
            }
        }
        context.SaveChanges();
    }
}
