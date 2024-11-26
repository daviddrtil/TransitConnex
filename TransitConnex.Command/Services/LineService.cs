using TransitConnex.Command.Commands.Line;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Line;
using TransitConnex.Domain.Models;

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

    public async Task<Line> CreateLine(LineCreateCommand createCommand)
    {
        var newLine = new Line
        {
            Label = createCommand.Label, Name = createCommand.Name, LineType = createCommand.LineType,
        };
        
        await lineRepository.Add(newLine);
        
        return newLine;
    }

    public async Task<List<Line>> CreateLines(List<LineCreateCommand> createCommands)
    {
        var newLines = new List<Line>();
        foreach (var createCommand in createCommands)
        {
            var newLine = new Line
            {
                Label = createCommand.Label, Name = createCommand.Name, LineType = createCommand.LineType,
            };
            newLines.Add(newLine);
        }
        
        await lineRepository.AddBatch(newLines);
        
        return newLines;
    }

    public Task<Line> EditLine(LineUpdateCommand editCommand)
    {
        throw new NotImplementedException();
    }

    public Task DeleteLine(Guid id)
    {
        throw new NotImplementedException();
    }
}
