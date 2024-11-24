using TransitConnex.Command.Data;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Seeds;

public class IconSeed
{
    public static void Seed(AppDbContext context)
    {
        var iconsToBeSeeded = new List<Icon>
        {
            new()
            {
                Name = "Bus",
                Svg =
                    "PHN2ZyB3aWR0aD0iNjQiIGhlaWdodD0iNjQiIHZpZXdCb3g9IjAgMCA2NCA2NCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB4PSI4IiB5PSIxNiIgd2lkdGg9IjQ4IiBoZWlnaHQ9IjMyIiByeD0iNCIgcnk9IjQiIGZpbGw9IiMwMDAiLz48Y2lyY2xlIGN4PSIyMCIgY3k9IjUyIiByPSI0IiBmaWxsPSIjMDAwIi8+PGNpcmNsZSBjeD0iNDQiIGN5PSI1MiIgcj0iNCIgZmlsbD0iIzAwMCIvPjxyZWN0IHg9IjE2IiB5PSIyMCIgd2lkdGg9IjMyIiBoZWlnaHQ9IjIwIiBmaWxsPSIjZmZmIi8+PC9zdmc+"
            },
            new()
            {
                Name = "Tram",
                Svg =
                    "PHN2ZyB3aWR0aD0iNjQiIGhlaWdodD0iNjQiIHZpZXdCb3g9IjAgMCA2NCA2NCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB4PSIxMCIgeT0iMjAiIHdpZHRoPSI0NCIgaGVpZ2h0PSIyNCIgcng9IjQiIHJ5PSI0IiBmaWxsPSIjMDAwIi8+PGNpcmNsZSBjeD0iMjAiIGN5PSI1MCIgcj0iNCIgZmlsbD0iIzAwMCIvPjxjaXJjbGUgY3g9IjQ0IiBjeT0iNTAiIHI9IjQiIGZpbGw9IiMwMDAiLz48cmVjdCB4PSIxOCIgeT0iMjQiIHdpZHRoPSIyOCIgaGVpZ2h0PSIxNiIgZmlsbD0iI2ZmZiIvPjwvc3ZnPg=="
            },
            new()
            {
                Name = "Train",
                Svg =
                    "PHN2ZyB3aWR0aD0iNjQiIGhlaWdodD0iNjQiIHZpZXdCb3g9IjAgMCA2NCA2NCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj48cmVjdCB4PSIxMiIgeT0iMTIiIHdpZHRoPSI0MCIgaGVpZ2h0PSI0MCIgcng9IjQiIHJ5PSI0IiBmaWxsPSIjMDAwIi8+PGNpcmNsZSBjeD0iMjAiIGN5PSI1NCIgcj0iNCIgZmlsbD0iIzAwMCIvPjxjaXJjbGUgY3g9IjQ0IiBjeT0iNTQiIHI9IjQiIGZpbGw9IiMwMDAiLz48cmVjdCB4PSIxOCIgeT0iMTgiIHdpZHRoPSIyOCIgaGVpZ2h0PSIyNCIgZmlsbD0iI2ZmZiIvPjwvc3ZnPg=="
            }
        };

        foreach (var icon in iconsToBeSeeded)
        {
            if (!context.Icons.Any(i => i.Name == icon.Name))
            {
                context.Icons.Add(icon);
            }
        }

        context.SaveChanges();
    }
}
