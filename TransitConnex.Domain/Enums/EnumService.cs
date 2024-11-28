using System.ComponentModel;
using System.Reflection;

namespace TransitConnex.Domain.Enums;

public static class EnumService
{
    public static List<string> GetEnumDescriptions<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e => e.GetType()
                             .GetField(e.ToString())?
                             .GetCustomAttribute<DescriptionAttribute>()?.Description
                         ?? e.ToString())
            .ToList();
    }
}
