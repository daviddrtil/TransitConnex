using Microsoft.AspNetCore.Mvc;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Command.Commands.Seat;

namespace TransitConnex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeatController(SeatCommandHandler seatCommandHandler) : Controller
{
    [HttpPost]
    public async Task<Guid> CreateSeat(SeatCreateCommand createCommand)
    {
        return await seatCommandHandler.HandleCreate(createCommand);
    }

    [HttpPost("batch")]
    public async Task<Guid> CreateSeats(List<SeatCreateCommand> createCommands)
    {
        // return await SeatCommandHandler.HandleCreate(createCommand);
        return new Guid(); // TODO
    }

    [HttpPost("reserve")] // TODO -> move to ScheduledRoute schema
    public async Task ReserveSeats(SeatReservationCommand reserveCommand)
    {
        await seatCommandHandler.HandleSeatReservation(reserveCommand);
    }
    
    [HttpPost("free")] // TODO -> move to ScheduledRoute schema
    public async Task FreeSeats(SeatReservationCommand freeCommand)
    {
        await seatCommandHandler.HandleSeatFree(freeCommand);
    }

    [HttpPut]
    public async Task<IActionResult> EditSeat(SeatUpdateCommand updateCommand)
    {
        await seatCommandHandler.HandleUpdate(updateCommand);

        return Ok();
    }

    [HttpPut("batch")]
    public async Task<IActionResult> EditScheduledRoutes(List<SeatUpdateCommand> updateCommand)
    {
        // TODO

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSeat(Guid id)
    {
        await seatCommandHandler.HandleDelete(id);

        return Ok();
    }

    [HttpDelete("batch")]
    public async Task<IActionResult> DeleteSeats(List<Guid> deleteIds)
    {
        // await SeatCommandHandler.HandleDelete(deleteCommand); // TODO

        return Ok();
    }
}
