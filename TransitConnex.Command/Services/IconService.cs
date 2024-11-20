using Microsoft.EntityFrameworkCore;
using TransitConnex.Domain.DTOs.Icon;
using TransitConnex.Domain.Mappings;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services
{
    public class IconService : IIconService
    {
        private readonly IIconRepository _iconRepository;

        public IconService(IIconRepository iconRepository)
        {
            _iconRepository = iconRepository;
        }

        public async Task<List<IconDto>> GetAllIcons()
        {
            return await _iconRepository.QueryAll().ToDto().ToListAsync();
        }

        public async Task<IconDto> GetIconById(Guid id)
        {
            return await _iconRepository.QueryById(id).ToDto().FirstOrDefaultAsync() ?? throw new KeyNotFoundException($"Icon with ID {id} was not found ds.");
        }

        public async Task<IconDto?> GetIconByName(string iconName) // TODO -> mby remake
        {
            return await _iconRepository.QueryByName(iconName).ToDto().FirstOrDefaultAsync();
        }

        public async Task<bool> IconExists(Guid id)
        {
            return await _iconRepository.QueryById(id).AnyAsync();
        }
        
        public async Task<IconDto> CreateIcon(IconCreateDto iconDto)
        {
            var newIcon = new Icon {Name = iconDto.Name, Svg = iconDto.Svg};
            
            await _iconRepository.Add(newIcon);
            
            return await GetIconById(newIcon.Id);
        }

        public async Task<IconDto> EditIcon(Guid id, IconCreateDto editedIcon)
        {
            var icon = await _iconRepository.QueryById(id).FirstOrDefaultAsync();

            if (icon == null)
            {
                throw new KeyNotFoundException($"Icon with ID {id} was not found.");
            }
            
            icon.Svg = editedIcon.Svg;
            icon.Name = editedIcon.Name;
            
            return await GetIconById(icon.Id);
        }

        public async Task DeleteIcon(Guid id)
        {
            var icon = await _iconRepository.QueryById(id).FirstOrDefaultAsync();

            if (icon == null)
            {
                throw new KeyNotFoundException($"Icon with ID {id} was not found.");
            }
            
            await _iconRepository.Delete(icon);
        }
    }
}
