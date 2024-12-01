using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Commands.Line;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Line;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services;

public class LineService(IMapper mapper, ILineRepository lineRepository) : ILineService
{
    public async Task<List<LineDto>> GetLinesFiltered(LineFilteredQuery filter)
    {
        var query = lineRepository.QueryAll();

        if (filter.LineType != null)
        {
            query = query.Where(l => l.LineType == filter.LineType);
        }

        if (filter.Name != null)
        {
            query = query.Where(l => l.Name != null && l.Name.Contains(filter.Name));
        }
        
        var lines = await query.ToListAsync();
        return mapper.Map<List<LineDto>>(lines);
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

    public async Task<Line> EditLine(LineUpdateCommand editCommand)
    {
        var line = await lineRepository.QueryById(editCommand.Id).FirstOrDefaultAsync();

        if (line == null)
        {
            throw new KeyNotFoundException($"Line with ID: {editCommand.Id} does not exist");
        }
        
        line = mapper.Map(editCommand, line);
        await lineRepository.Update(line);
        
        return line;
    }

    public async Task<List<Line>> EditLines(List<LineUpdateCommand> editCommand)
    {
        var lineIds = editCommand.Select(x => x.Id).ToList();
        var lines = await lineRepository.QueryAll().Where(x => lineIds.Contains(x.Id)).ToListAsync();
        if (lines.Count != lineIds.Count)
        {
            var missing = lineIds.Except(lines.Select(x => x.Id)).ToList();
            throw new KeyNotFoundException($"Lines with IDs: [{string.Join(", ", missing.ToArray())}] were not found.");
        }
        
        lines = mapper.Map(editCommand, lines);
        await lineRepository.UpdateBatch(lines);

        return lines;
    }

    public async Task DeleteLine(Guid id)
    {
        var line = await lineRepository.QueryById(id).FirstOrDefaultAsync();
        
        if (line == null)
        {
            throw new KeyNotFoundException($"Line with ID: {id} does not exist");
        }
        
        await lineRepository.Delete(line);
    }
}
