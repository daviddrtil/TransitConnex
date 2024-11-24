using TransitConnex.Domain.DTOs.Line;

namespace TransitConnex.Command.Services.Interfaces;

public interface ILineService
{
    Task<List<LineDto>> GetAllLines();

    Task<LineDto> GetLineById(Guid id);

    Task<bool> LineExists(Guid id);

    Task<LineDto> CreateLine(LineCreateDto lineDto);

    Task<LineDto> EditLine(Guid id, LineCreateDto editedLine);

    Task DeleteLine(Guid id);
}
