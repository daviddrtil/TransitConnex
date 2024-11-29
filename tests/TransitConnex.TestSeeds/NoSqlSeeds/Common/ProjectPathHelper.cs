namespace TransitConnex.TestSeeds.NoSqlSeeds.Common;

internal static class ProjectPathHelper
{
    public static string GetSolutionDirectory()
    {
        string? directory = AppDomain.CurrentDomain.BaseDirectory;

        // Traverse up to the solution directory
        while (directory != null && Directory.GetFiles(directory, "*.sln").Length == 0)
        {
            directory = Directory.GetParent(directory)?.FullName;
        }

        if (directory == null)
        {
            throw new InvalidOperationException("Solution directory not found.");
        }

        return directory;
    }
}
