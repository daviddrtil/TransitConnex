using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.Line;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class LineCommandHandler : IBaseCommandHandler<ILineCommand>
    {
        private readonly ILineService _lineService;

        public LineCommandHandler(ILineService lineService)
        {
            _lineService = lineService;
        }
        
        public Task HandleCreate(ILineCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdate(ILineCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleDelete(ILineCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
