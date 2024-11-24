using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Icon;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Icon;
using TransitConnex.Domain.Mappings;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class IconService(IIconRepository iconRepository) : IIconService
{
    public async Task<List<IconDto>> GetAllIcons()
    {
        return await iconRepository.QueryAll().ToDto().ToListAsync();
    }

    public async Task<IconDto> GetIconById(Guid id)
    {
        return await iconRepository.QueryById(id).ToDto().FirstOrDefaultAsync() ??
               throw new KeyNotFoundException($"Icon with ID {id} was not found ds.");
    }

    public async Task<IconDto?> GetIconByName(string iconName) // TODO -> mby remake -> return list?
    {
        return await iconRepository.QueryByName(iconName).ToDto().FirstOrDefaultAsync();
    }

    public async Task<bool> IconExists(Guid id)
    {
        return await iconRepository.QueryById(id).AnyAsync();
    }

    public async Task<Icon> CreateIcon(IconCreateCommand createCommand)
    {
        var newIcon = new Icon { Name = createCommand.Name, Svg = createCommand.Svg };

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

        await iconRepository.Update(icon, updateCommand);

        return icon;
    }

    public async Task DeleteIcon(IconDeleteCommand deleteCommand) // TODO -> fails on delete cascade even tho i set it ot setNull??????
    {
        var icon = await iconRepository.QueryById(deleteCommand.Id).FirstOrDefaultAsync();

        if (icon == null)
        {
            throw new KeyNotFoundException($"Icon with ID {deleteCommand.Id} was not found.");
        }

        await iconRepository.Delete(icon);
    }
}
