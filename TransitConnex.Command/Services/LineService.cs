using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Line;

namespace TransitConnex.Command.Services;

public class LineService(ILineRepository lineRepository) : ILineService
{
    public Task<List<LineDto>> GetAllLines()
    {
        throw new NotImplementedException();
    }

    public Task<LineDto> GetLineById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> LineExists(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<LineDto> CreateLine(LineCreateDto lineDto)
    {
        throw new NotImplementedException();
    }

    public Task<LineDto> EditLine(Guid id, LineCreateDto editedLine)
    {
        throw new NotImplementedException();
    }

    public Task DeleteLine(Guid id)
    {
        throw new NotImplementedException();
    }
}
