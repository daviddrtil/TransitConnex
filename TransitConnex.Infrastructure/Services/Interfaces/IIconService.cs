using TransitConnex.Domain.DTOs.Icon;

namespace TransitConnex.Infrastructure.Services.Interfaces
{
    public interface IIconService
    {
        Task<List<IconDto>> GetAllIcons();

        Task<IconDto> GetIconById(Guid id);

        Task<IconDto?> GetIconByName(string iconName);

        Task<bool> IconExists(Guid id);

        Task<IconDto> CreateIcon(IconCreateDto iconDto);

        Task<IconDto> EditIcon(Guid id, IconCreateDto editedIcon);
        
        Task DeleteIcon(Guid id);
    }
}
