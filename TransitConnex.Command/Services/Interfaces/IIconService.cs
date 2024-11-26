using TransitConnex.Command.Commands.Icon;
using TransitConnex.Domain.DTOs.Icon;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface IIconService
{
    Task<List<IconDto>> GetAllIcons();

    Task<IconDto> GetIconById(Guid id);

    Task<IconDto?> GetIconByName(string iconName);

    Task<bool> IconExists(Guid id);

    Task<Icon> CreateIcon(IconCreateCommand createCommand);

    Task<Icon> EditIcon(IconUpdateCommand updateCommand);

    Task DeleteIcon(Guid id);
}
