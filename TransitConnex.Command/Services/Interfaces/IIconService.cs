using TransitConnex.Domain.DTOs.Icon;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Icon;

namespace TransitConnex.Infrastructure.Services.Interfaces;

public interface IIconService
{
    Task<List<IconDto>> GetAllIcons();

    Task<IconDto> GetIconById(Guid id);

    Task<IconDto?> GetIconByName(string iconName);

    Task<bool> IconExists(Guid id);

    Task<Icon> CreateIcon(IconCreateCommand createCommand);

    Task<Icon> EditIcon(IconUpdateCommand updateCommand);

    Task DeleteIcon(IconDeleteCommand deleteCommand);
}
