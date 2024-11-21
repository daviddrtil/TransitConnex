using TransitConnex.API.Handlers.CommandHandlers.Common;
using TransitConnex.Infrastructure.Commands.Seat;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.API.Handlers.CommandHandlers
{
    public class SeatCommandHandler : IBaseCommandHandler<ISeatCommand>
    {
        private readonly ISeatService _seatService;

        public SeatCommandHandler(ISeatService seatService)
        {
            _seatService = seatService;
        }
        
        public Task HandleCreate(ISeatCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleUpdate(ISeatCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleDelete(ISeatCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
