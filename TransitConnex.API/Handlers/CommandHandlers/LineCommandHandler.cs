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

        return new Guid();
    }

    public async Task HandleUpdate(ILineCommand command)
    {
        if (command is not LineUpdateCommand lineUpdateCommand)
        {
            throw new InvalidCastException();
        }
    }

    public async Task HandleDelete(ILineCommand command)
    {
        if (command is not LineDeleteCommand lineDeleteCommand)
        {
            throw new InvalidCastException();
        }
    }
}
