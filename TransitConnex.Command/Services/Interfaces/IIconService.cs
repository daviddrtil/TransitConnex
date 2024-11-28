using TransitConnex.Command.Commands.Icon;
using TransitConnex.Domain.DTOs.Icon;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services.Interfaces;

public interface IIconService
{
    Task<List<IconDto>> GetFilteredIcons(IconFilteredQuery filter);

    Task<IconDto?> GetIconByName(string iconName);

    Task<bool> IconExists(Guid id);

    Task<Icon> CreateIcon(IconCreateCommand createCommand);

    Task<Icon> EditIcon(IconUpdateCommand updateCommand);

    Task DeleteIcon(Guid id);
}
