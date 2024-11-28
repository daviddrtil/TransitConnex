using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Icon;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Icon;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Mappings;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services;

public class IconService(IMapper mapper ,IIconRepository iconRepository) : IIconService
{
    public async Task<List<IconDto>> GetFilteredIcons(IconFilteredQuery filter)
    {
        var query = iconRepository.QueryAll();
        if (filter.Name is not null)
        {
            var normalizedFilterName = filter.Name.ToLower();
            query = query.Where(x => x.Name != null && x.Name.ToLower().Contains(normalizedFilterName));
        }

        if (filter.VehicleIcons)
        {
            var vehicleNames = EnumService.GetEnumDescriptions<VehicleTypeEnum>();
            query = query.Where(x => x.Name != null && vehicleNames.Contains(x.Name));
        }

        if (filter.Ids is not null)
        {
            query = query.Where(x => filter.Ids.Contains(x.Id));
        }
        
        var icons = await query.ToListAsync();
        
        return mapper.Map<List<IconDto>>(icons);
    }

    public async Task<IconDto?> GetIconByName(string iconName)
    {
        return await iconRepository.QueryByName(iconName).ToDto().FirstOrDefaultAsync();
    }

    public async Task<bool> IconExists(Guid id)
    {
        return await iconRepository.QueryById(id).AnyAsync();
    }

    public async Task<Icon> CreateIcon(IconCreateCommand createCommand)
    {
        var newIcon = mapper.Map<Icon>(createCommand);
        await iconRepository.Add(newIcon);

        return newIcon;
    }

    public async Task<Icon> EditIcon(IconUpdateCommand updateCommand)
    {
        var icon = await iconRepository.QueryById(updateCommand.Id).FirstOrDefaultAsync();

        if (icon == null)
        {
            throw new KeyNotFoundException($"Icon with ID {updateCommand.Id} was not found.");
        }

        icon = mapper.Map(updateCommand, icon);
        await iconRepository.Update(icon);

        return icon;
    }

    public async Task DeleteIcon(Guid id)
    {
        var icon = await iconRepository.QueryById(id).FirstOrDefaultAsync();

        if (icon == null)
        {
            throw new KeyNotFoundException($"Icon with ID {id} was not found.");
        }

        await iconRepository.Delete(icon);
    }
}
