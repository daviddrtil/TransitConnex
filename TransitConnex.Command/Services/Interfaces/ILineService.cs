using TransitConnex.Command.Commands.Line;
using TransitConnex.Domain.DTOs.Line;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface ILineService
{
    Task<List<LineDto>> GetLinesFiltered();
    
    Task<Line> CreateLine(LineCreateCommand createCommand);
    Task<List<Line>> CreateLines(List<LineCreateCommand> createCommands);
    Task<Line> EditLine(LineUpdateCommand editCommand);
    Task<List<Line>> EditLines(List<LineUpdateCommand> editCommand);
    Task DeleteLine(Guid id);
}
