using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Command.Commands.Line;
using TransitConnex.Command.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers;

public class LineCommandHandler(ILineService lineService) : IBaseCommandHandler<ILineCommand>
{
    private readonly ILineService _lineService = lineService;

    public async Task<Guid> HandleCreate(ILineCommand command)
    {
        if (command is not LineCreateCommand lineCreateCommand)
        {
            throw new InvalidCastException();
        }

        var created = await lineService.CreateLine(lineCreateCommand);
        
        return created.Id;
    }

    public async Task<List<Guid>> HandleBatchCreate(ILineCommand command)
    {
        if (command is not LineBatchCreateCommand batchCreateCommand)
        {
            throw new InvalidCastException($"Invalid command given, expected {nameof(LineBatchCreateCommand)}");
        }
            
        var created = await lineService.CreateLines(batchCreateCommand.Lines);
        
        return created.Select(x => x.Id).ToList();
    }

    public async Task HandleUpdate(ILineCommand command) // TODO
    {
        if (command is not LineUpdateCommand updateCommand)
        {
            throw new InvalidCastException();
        }
        
        var updated = await lineService.EditLine(updateCommand);
    }

    public async Task HandleDelete(ILineCommand command)
    {
        
    }
    
    public async Task HandleDelete(Guid id) // TODO
    {
        await lineService.DeleteLine(id);
    }
}
